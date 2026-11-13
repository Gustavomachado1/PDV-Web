using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PDV.Domain;
using PDV.Infrastructure;

namespace PDV.API.Servicos;

public sealed record LoginRequest(
    string CodigoEmpresa,
    string Login,
    string Senha,
    string? Dispositivo = null);

public sealed record SessaoLogin(string AccessToken, DateTime ExpiraEmUtc);

public sealed class AutenticacaoService(
    PdvDbContext db,
    IPasswordHasher<Usuario> passwordHasher,
    EmpresaContextoLogin empresaContexto,
    TimeProvider timeProvider,
    ILogger<AutenticacaoService> logger)
{
    private const int MaxTentativasFalhas = 5;
    private static readonly TimeSpan DuracaoBloqueio = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan DuracaoSessao = TimeSpan.FromHours(8);

    public async Task<SessaoLogin?> EntrarAsync(
        LoginRequest request, string? ip, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.CodigoEmpresa) ||
            string.IsNullOrWhiteSpace(request.Login) ||
            string.IsNullOrEmpty(request.Senha) ||
            request.CodigoEmpresa.Trim().Length > 30 ||
            request.Login.Trim().Length > 100 ||
            request.Senha.Length > 1024)
            return null;

        var codigoEmpresa = request.CodigoEmpresa.Trim();
        var login = request.Login.Trim();
        var empresa = await db.Empresas.IgnoreQueryFilters()
            .SingleOrDefaultAsync(e => e.Codigo == codigoEmpresa && e.Ativa, cancellationToken);

        if (empresa is null)
        {
            logger.LogWarning("Tentativa de autenticação para uma empresa inexistente. Código: {CodigoEmpresa}, login: {Login}, IP: {Ip}",
                codigoEmpresa, login, ip);
            return null;
        }

        empresaContexto.Definir(empresa.Id);

        var usuario = await db.Usuarios.IgnoreQueryFilters()
            .SingleOrDefaultAsync(u => u.EmpresaId == empresa.Id && u.Login == login, cancellationToken);
        var agora = timeProvider.GetUtcNow().UtcDateTime;

        if (usuario is null)
        {
            db.LogsAutenticacao.Add(CriarLog(empresa.Id, null, login, false, "Credenciais inválidas", ip, agora));
            await db.SaveChangesAsync(cancellationToken);
            return null;
        }

        empresaContexto.DefinirUsuario(usuario.Id);

        if (usuario.BloqueadoAteUtc is DateTime bloqueadoAte && bloqueadoAte > agora)
        {
            db.LogsAutenticacao.Add(CriarLog(empresa.Id, usuario.Id, login, false, "Usuário temporariamente bloqueado", ip, agora));
            await db.SaveChangesAsync(cancellationToken);
            return null;
        }

        if (usuario.BloqueadoAteUtc is not null)
        {
            usuario.TentativasFalhas = 0;
            usuario.BloqueadoAteUtc = null;
        }

        var senhaValida = VerificarSenha(usuario, request.Senha);
        if (usuario.Status != StatusUsuario.Ativo || !senhaValida)
        {
            if (usuario.Status == StatusUsuario.Ativo)
            {
                usuario.TentativasFalhas++;
                if (usuario.TentativasFalhas >= MaxTentativasFalhas)
                    usuario.BloqueadoAteUtc = agora.Add(DuracaoBloqueio);
            }

            var motivo = usuario.Status == StatusUsuario.Ativo
                ? "Credenciais inválidas"
                : "Usuário inativo ou bloqueado";
            db.LogsAutenticacao.Add(CriarLog(empresa.Id, usuario.Id, login, false, motivo, ip, agora));
            await db.SaveChangesAsync(cancellationToken);
            return null;
        }

        usuario.TentativasFalhas = 0;
        usuario.BloqueadoAteUtc = null;

        var token = CriarToken();
        var expiraEmUtc = agora.Add(DuracaoSessao);
        var sessao = new Sessao
        {
            EmpresaId = empresa.Id,
            UsuarioId = usuario.Id,
            TokenHash = HashToken(token),
            Dispositivo = request.Dispositivo?.Length > 200 ? request.Dispositivo[..200] : request.Dispositivo,
            ExpiraEmUtc = expiraEmUtc
        };

        db.Sessoes.Add(sessao);
        db.LogsAutenticacao.Add(CriarLog(empresa.Id, usuario.Id, login, true, null, ip, agora));
        await db.SaveChangesAsync(cancellationToken);

        return new SessaoLogin(token, expiraEmUtc);
    }

    public async Task<bool> EncerrarAsync(Guid sessaoId, CancellationToken cancellationToken)
    {
        var sessao = await db.Sessoes.SingleOrDefaultAsync(s => s.Id == sessaoId, cancellationToken);
        if (sessao is null)
            return false;

        if (sessao.EncerradaEmUtc is null)
        {
            sessao.EncerradaEmUtc = timeProvider.GetUtcNow().UtcDateTime;
            await db.SaveChangesAsync(cancellationToken);
        }

        return true;
    }

    internal static string HashToken(string token)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    private bool VerificarSenha(Usuario usuario, string senha)
    {
        if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
            return false;

        try
        {
            var resultado = passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);
            if (resultado == PasswordVerificationResult.SuccessRehashNeeded)
                usuario.SenhaHash = passwordHasher.HashPassword(usuario, senha);

            return resultado is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "Hash de senha inválido para o usuário {UsuarioId}.", usuario.Id);
            return false;
        }
    }

    private static LogAutenticacao CriarLog(
        Guid empresaId, Guid? usuarioId, string login, bool sucesso, string? motivo, string? ip, DateTime ocorridoEmUtc)
        => new()
        {
            EmpresaId = empresaId,
            UsuarioId = usuarioId,
            Login = login,
            Sucesso = sucesso,
            MotivoFalha = motivo,
            Ip = ip,
            OcorridoEmUtc = ocorridoEmUtc
        };

    private static string CriarToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
}
