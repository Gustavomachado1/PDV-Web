using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.API.Tests;

public class AutenticacaoServiceTests
{
    [Fact]
    public async Task LoginCriaSessaoOpacaComHashEClaimsDeEmpresa()
    {
        await using var contexto = await CriarContextoAsync();

        var resultado = await contexto.Autenticacao.EntrarAsync(
            new LoginRequest("empresa-teste", "operador", "SenhaForte!123"),
            "127.0.0.1",
            CancellationToken.None);

        Assert.NotNull(resultado);
        Assert.Equal(contexto.Agora.AddHours(8).UtcDateTime, resultado.ExpiraEmUtc);

        var sessao = await contexto.Db.Sessoes.IgnoreQueryFilters().SingleAsync();
        var hashEsperado = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(resultado.AccessToken)));
        Assert.Equal(hashEsperado, sessao.TokenHash);
        Assert.NotEqual(resultado.AccessToken, sessao.TokenHash);

        var handler = CriarHandler(contexto, resultado.AccessToken);
        var autenticacao = await ((IAuthenticationHandler)handler).AuthenticateAsync();
        Assert.True(autenticacao.Succeeded);
        Assert.Equal(contexto.Usuario.Id.ToString(), autenticacao.Principal!.FindFirst("sub")!.Value);
        Assert.Equal(contexto.Empresa.Id.ToString(), autenticacao.Principal.FindFirst("empresa_id")!.Value);

        var autenticacaoHub = await ((IAuthenticationHandler)CriarHandler(contexto, resultado.AccessToken, true))
            .AuthenticateAsync();
        Assert.True(autenticacaoHub.Succeeded);

        var log = await contexto.Db.LogsAutenticacao.SingleAsync();
        Assert.True(log.Sucesso);
        Assert.Equal(contexto.Usuario.Id, log.UsuarioId);
        Assert.Null(log.MotivoFalha);
    }

    [Fact]
    public async Task QuintaFalhaBloqueiaPorQuinzeMinutosELoginValidoLiberaAConta()
    {
        await using var contexto = await CriarContextoAsync();

        for (var tentativa = 1; tentativa <= 5; tentativa++)
        {
            var resultado = await contexto.Autenticacao.EntrarAsync(
                new LoginRequest("empresa-teste", "operador", "senha-incorreta"),
                null,
                CancellationToken.None);
            Assert.Null(resultado);
        }

        Assert.Equal(5, contexto.Usuario.TentativasFalhas);
        Assert.Equal(contexto.Agora.AddMinutes(15).UtcDateTime, contexto.Usuario.BloqueadoAteUtc);

        var resultadoBloqueado = await contexto.Autenticacao.EntrarAsync(
            new LoginRequest("empresa-teste", "operador", "SenhaForte!123"),
            null,
            CancellationToken.None);
        Assert.Null(resultadoBloqueado);
        Assert.Equal(6, await contexto.Db.LogsAutenticacao.CountAsync());

        contexto.Relogio.Avancar(TimeSpan.FromMinutes(15));
        var resultadoValido = await contexto.Autenticacao.EntrarAsync(
            new LoginRequest("empresa-teste", "operador", "SenhaForte!123"),
            null,
            CancellationToken.None);

        Assert.NotNull(resultadoValido);
        Assert.Equal(0, contexto.Usuario.TentativasFalhas);
        Assert.Null(contexto.Usuario.BloqueadoAteUtc);
    }

    [Fact]
    public async Task LogoutRevogaTokenERegistraEncerramento()
    {
        await using var contexto = await CriarContextoAsync();
        var resultado = await contexto.Autenticacao.EntrarAsync(
            new LoginRequest("empresa-teste", "operador", "SenhaForte!123"),
            null,
            CancellationToken.None);
        Assert.NotNull(resultado);

        var sessao = await contexto.Db.Sessoes.IgnoreQueryFilters().SingleAsync();
        Assert.True(await contexto.Autenticacao.EncerrarAsync(sessao.Id, CancellationToken.None));

        var autenticacao = await ((IAuthenticationHandler)CriarHandler(contexto, resultado.AccessToken))
            .AuthenticateAsync();
        Assert.False(autenticacao.Succeeded);
        Assert.NotNull(sessao.EncerradaEmUtc);
        Assert.Equal(contexto.Agora.UtcDateTime, sessao.EncerradaEmUtc);
    }

    [Fact]
    public async Task SessaoExpiradaNaoAutentica()
    {
        await using var contexto = await CriarContextoAsync();
        var resultado = await contexto.Autenticacao.EntrarAsync(
            new LoginRequest("empresa-teste", "operador", "SenhaForte!123"),
            null,
            CancellationToken.None);
        Assert.NotNull(resultado);

        var sessao = await contexto.Db.Sessoes.IgnoreQueryFilters().SingleAsync();
        sessao.ExpiraEmUtc = contexto.Agora.UtcDateTime;
        await contexto.Db.SaveChangesAsync();

        var autenticacao = await ((IAuthenticationHandler)CriarHandler(contexto, resultado.AccessToken))
            .AuthenticateAsync();
        Assert.False(autenticacao.Succeeded);
    }

    private static SessaoAuthenticationHandler CriarHandler(
        ContextoTeste contexto, string token, bool usarTokenDoHub = false)
    {
        var handler = new SessaoAuthenticationHandler(
            new OptionsMonitorFalso<AuthenticationSchemeOptions>(new AuthenticationSchemeOptions()),
            NullLoggerFactory.Instance,
            System.Text.Encodings.Web.UrlEncoder.Default,
            contexto.Db,
            contexto.Relogio);
        var http = new DefaultHttpContext();
        if (usarTokenDoHub)
        {
            http.Request.Path = "/hubs/notificacoes";
            http.Request.QueryString = new QueryString($"?access_token={Uri.EscapeDataString(token)}");
        }
        else
        {
            http.Request.Headers.Authorization = $"Bearer {token}";
        }
        handler.InitializeAsync(new AuthenticationScheme("Sessao", "Sessao", typeof(SessaoAuthenticationHandler)), http)
            .GetAwaiter().GetResult();
        return handler;
    }

    private static async Task<ContextoTeste> CriarContextoAsync()
    {
        var relogio = new RelogioFalso(new DateTimeOffset(2026, 11, 12, 12, 0, 0, TimeSpan.Zero));
        var empresaContexto = new EmpresaContextoLogin();
        var usuarioAtual = new UsuarioAtualFalso(empresaContexto);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .AddInterceptors(new AuditoriaInterceptor(usuarioAtual))
            .Options;
        var db = new PdvDbContext(opcoes, usuarioAtual);

        var empresa = new Empresa
        {
            Codigo = "empresa-teste",
            RazaoSocial = "Empresa de teste",
            Cnpj = "12345678000199"
        };
        db.Empresas.Add(empresa);
        await db.SaveChangesAsync();
        empresaContexto.Definir(empresa.Id);

        var perfil = new Perfil { EmpresaId = empresa.Id, Nome = "Operador", Tipo = TipoPerfil.Operador };
        db.Perfis.Add(perfil);
        await db.SaveChangesAsync();

        var hasher = new PasswordHasher<Usuario>();
        var usuario = new Usuario
        {
            EmpresaId = empresa.Id,
            Nome = "Operador",
            Login = "operador",
            SenhaHash = "",
            PerfilId = perfil.Id
        };
        usuario.SenhaHash = hasher.HashPassword(usuario, "SenhaForte!123");
        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();
        empresaContexto.Limpar();

        var autenticacao = new AutenticacaoService(
            db, hasher, empresaContexto, relogio, NullLogger<AutenticacaoService>.Instance);
        return new ContextoTeste(db, empresa, usuario, autenticacao, relogio);
    }

    private sealed class ContextoTeste(
        PdvDbContext db,
        Empresa empresa,
        Usuario usuario,
        AutenticacaoService autenticacao,
        RelogioFalso relogio) : IAsyncDisposable
    {
        public PdvDbContext Db { get; } = db;
        public Empresa Empresa { get; } = empresa;
        public Usuario Usuario { get; } = usuario;
        public AutenticacaoService Autenticacao { get; } = autenticacao;
        public RelogioFalso Relogio { get; } = relogio;
        public DateTimeOffset Agora => Relogio.GetUtcNow();

        public ValueTask DisposeAsync() => Db.DisposeAsync();
    }

    private sealed class UsuarioAtualFalso(EmpresaContextoLogin empresaContexto) : IUsuarioAtual
    {
        public Guid? UsuarioId => empresaContexto.UsuarioId;
        public Guid? EmpresaId => empresaContexto.EmpresaId;
        public Guid? FilialId => empresaContexto.FilialId;
    }

    private sealed class RelogioFalso(DateTimeOffset agora) : TimeProvider
    {
        private DateTimeOffset _agora = agora;

        public override DateTimeOffset GetUtcNow() => _agora;
        public void Avancar(TimeSpan intervalo) => _agora = _agora.Add(intervalo);
    }

    private sealed class OptionsMonitorFalso<T>(T options) : IOptionsMonitor<T> where T : class
    {
        public T CurrentValue => options;
        public T Get(string? name) => options;
        public IDisposable? OnChange(Action<T, string?> listener) => null;
    }
}
