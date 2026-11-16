using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;
using Xunit;

namespace PDV.API.Tests;

public class ProvisionamentoInicialServiceTests
{
    [Fact]
    public async Task ProvisionaEmpresaFilialAdministradorPerfisPermissoesEPlano()
    {
        await using var contexto = await CriarContextoAsync();

        await contexto.Servico.ExecutarAsync(CriarConfiguracao(), CancellationToken.None);

        var empresa = await contexto.Db.Empresas.IgnoreQueryFilters().SingleAsync();
        var filial = await contexto.Db.Filiais.IgnoreQueryFilters().SingleAsync();
        var usuario = await contexto.Db.Usuarios.IgnoreQueryFilters().SingleAsync();
        var perfis = await contexto.Db.Perfis.IgnoreQueryFilters().ToListAsync();
        var permissoes = await contexto.Db.Permissoes.ToListAsync();
        var vinculosPermissao = await contexto.Db.PerfisPermissoes.IgnoreQueryFilters().ToListAsync();
        var plano = await contexto.Db.Planos.SingleAsync();

        Assert.Equal("empresa-teste", empresa.Codigo);
        Assert.Equal("11222333000181", empresa.Cnpj);
        Assert.Equal(empresa.Id, filial.EmpresaId);
        Assert.Equal("filial-1", filial.Codigo);
        Assert.Equal(4, perfis.Count);
        Assert.Contains(perfis, perfil => perfil.Nome == "Gerente" && perfil.Tipo == TipoPerfil.Gerente);
        Assert.Contains(perfis, perfil => perfil.Nome == "Operador" && perfil.Tipo == TipoPerfil.Operador);
        Assert.Contains(perfis, perfil => perfil.Nome == "Estoquista" && perfil.Tipo == TipoPerfil.Estoquista);
        Assert.Equal(new[]
            {
                "catalogo.gerenciar",
                "estoque.configurar",
                "estoque.consultar",
                "estoque.inventario.gerenciar",
                "estoque.movimentar.solicitar",
                "estoque.movimento.entrada.autorizar",
                "estoque.movimento.saida.autorizar",
                "usuarios.gerenciar",
                "venda.cancelar"
            },
            permissoes.Select(permissao => permissao.Codigo).Order().ToArray());
        var perfilAdministrador = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Administrador);
        var vinculosAdministrador = vinculosPermissao
            .Where(vinculo => vinculo.PerfilId == perfilAdministrador.Id).ToArray();
        Assert.Equal(permissoes.Select(permissao => permissao.Id).Order(),
            vinculosAdministrador.Select(vinculo => vinculo.PermissaoId).Order());
        var perfilGerente = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Gerente);
        Assert.Contains(vinculosPermissao, vinculo =>
            vinculo.PerfilId == perfilGerente.Id &&
            vinculo.PermissaoId == permissoes.Single(x => x.Codigo == "estoque.movimento.entrada.autorizar").Id);
        var perfilEstoquista = perfis.Single(perfil => perfil.Tipo == TipoPerfil.Estoquista);
        Assert.Contains(vinculosPermissao, vinculo =>
            vinculo.PerfilId == perfilEstoquista.Id &&
            vinculo.PermissaoId == permissoes.Single(x => x.Codigo == "estoque.inventario.gerenciar").Id);
        Assert.Equal(perfilAdministrador.Id, usuario.PerfilId);
        Assert.Equal(StatusUsuario.Ativo, usuario.Status);
        Assert.NotEqual("SenhaInicialMuitoSegura!123", usuario.SenhaHash);
        Assert.Equal(PasswordVerificationResult.Success,
            new PasswordHasher<Usuario>().VerifyHashedPassword(usuario, usuario.SenhaHash, "SenhaInicialMuitoSegura!123"));
        Assert.Equal("Essencial", plano.Nome);
        Assert.Equal((1, 2, 3), (plano.MaxFiliais, plano.MaxCaixas, plano.MaxUsuarios));

        var vinculoFilial = await contexto.Db.UsuariosFiliais.IgnoreQueryFilters().SingleAsync();
        Assert.Equal(usuario.Id, vinculoFilial.UsuarioId);
        Assert.Equal(filial.Id, vinculoFilial.FilialId);
    }

    [Fact]
    public void ConfiguracaoAusenteFalhaSemExporValores()
    {
        var erro = Assert.Throws<InvalidOperationException>(() =>
            ProvisionamentoInicialConfiguracao.Parse(new Dictionary<string, string?>()));

        Assert.Contains("PDV_BOOTSTRAP_EMPRESA_CNPJ", erro.Message);
        Assert.DoesNotContain("Senha", erro.Message);
    }

    [Theory]
    [InlineData("12345678000199", "senha-fraca")]
    [InlineData("11222333000181", "senha-com-controle\n!")]
    public void ConfiguracaoInvalidaFalhaAntesDeUsarBanco(string cnpj, string senha)
    {
        var variaveis = CriarVariaveis(cnpj, senha);

        var erro = Assert.Throws<InvalidOperationException>(() =>
            ProvisionamentoInicialConfiguracao.Parse(variaveis));

        Assert.DoesNotContain(senha, erro.Message);
    }

    [Fact]
    public async Task SegundaExecucaoFalhaSemCriarDadosDuplicados()
    {
        await using var contexto = await CriarContextoAsync();
        var configuracao = CriarConfiguracao();
        await contexto.Servico.ExecutarAsync(configuracao, CancellationToken.None);

        var erro = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            contexto.Servico.ExecutarAsync(configuracao, CancellationToken.None));

        Assert.Contains("já foi executado", erro.Message);
        Assert.Single(await contexto.Db.Empresas.IgnoreQueryFilters().ToListAsync());
        Assert.Single(await contexto.Db.Usuarios.IgnoreQueryFilters().ToListAsync());
        Assert.Single(await contexto.Db.Planos.ToListAsync());
    }

    private static ProvisionamentoInicialConfiguracao CriarConfiguracao()
        => ProvisionamentoInicialConfiguracao.Parse(CriarVariaveis("11.222.333/0001-81", "SenhaInicialMuitoSegura!123"));

    private static Dictionary<string, string?> CriarVariaveis(string cnpj, string senha)
        => new(StringComparer.Ordinal)
        {
            ["PDV_BOOTSTRAP_EMPRESA_CODIGO"] = " empresa-teste ",
            ["PDV_BOOTSTRAP_EMPRESA_RAZAO_SOCIAL"] = "Empresa de teste",
            ["PDV_BOOTSTRAP_EMPRESA_NOME_FANTASIA"] = "Loja de teste",
            ["PDV_BOOTSTRAP_EMPRESA_CNPJ"] = cnpj,
            ["PDV_BOOTSTRAP_FILIAL_CODIGO"] = "filial-1",
            ["PDV_BOOTSTRAP_FILIAL_NOME"] = "Matriz",
            ["PDV_BOOTSTRAP_ADMIN_NOME"] = "Administradora",
            ["PDV_BOOTSTRAP_ADMIN_LOGIN"] = "admin",
            ["PDV_BOOTSTRAP_ADMIN_EMAIL"] = "admin@example.test",
            ["PDV_BOOTSTRAP_ADMIN_SENHA"] = senha
        };

    private static async Task<ContextoTeste> CriarContextoAsync()
    {
        var empresaContexto = new EmpresaContextoLogin();
        var usuarioAtual = new UsuarioAtualFalso(empresaContexto);
        var opcoes = new DbContextOptionsBuilder<PdvDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .AddInterceptors(new AuditoriaInterceptor(usuarioAtual))
            .Options;
        var db = new PdvDbContext(opcoes, usuarioAtual);
        var hasher = new PasswordHasher<Usuario>();
        var servico = new ProvisionamentoInicialService(db, empresaContexto, hasher);
        return await Task.FromResult(new ContextoTeste(db, servico));
    }

    private sealed class ContextoTeste(PdvDbContext db, ProvisionamentoInicialService servico) : IAsyncDisposable
    {
        public PdvDbContext Db { get; } = db;
        public ProvisionamentoInicialService Servico { get; } = servico;
        public ValueTask DisposeAsync() => Db.DisposeAsync();
    }

    private sealed class UsuarioAtualFalso(EmpresaContextoLogin contexto) : IUsuarioAtual
    {
        public Guid? UsuarioId => contexto.UsuarioId;
        public Guid? EmpresaId => contexto.EmpresaId;
        public Guid? FilialId => contexto.FilialId;
    }
}
