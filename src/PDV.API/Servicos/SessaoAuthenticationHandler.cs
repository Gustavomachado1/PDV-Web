using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PDV.Domain;
using PDV.Infrastructure;
using System.Text.Encodings.Web;

namespace PDV.API.Servicos;

public sealed class SessaoAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    PdvDbContext db,
    TimeProvider timeProvider)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorization = Request.Headers.Authorization.ToString();
        string token;
        if (string.IsNullOrWhiteSpace(authorization))
        {
            if (!Request.Path.StartsWithSegments("/hubs", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.NoResult();

            token = Request.Query["access_token"].ToString();
        }
        else
        {
            if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Esquema de autenticação inválido.");

            token = authorization["Bearer ".Length..].Trim();
        }

        if (token.Length == 0)
            return string.IsNullOrWhiteSpace(authorization)
                ? AuthenticateResult.NoResult()
                : AuthenticateResult.Fail("Token de sessão inválido.");

        if (token.Length > 256)
            return AuthenticateResult.Fail("Token de sessão inválido.");

        var hash = AutenticacaoService.HashToken(token);
        var agora = timeProvider.GetUtcNow().UtcDateTime;
        var sessao = await db.Sessoes.IgnoreQueryFilters()
            .SingleOrDefaultAsync(s => s.TokenHash == hash &&
                s.EncerradaEmUtc == null && s.ExpiraEmUtc > agora, Context.RequestAborted);

        if (sessao is null)
            return AuthenticateResult.Fail("Sessão inválida ou expirada.");

        var usuario = await db.Usuarios.IgnoreQueryFilters()
            .SingleOrDefaultAsync(u => u.Id == sessao.UsuarioId &&
                u.EmpresaId == sessao.EmpresaId && u.Status == StatusUsuario.Ativo, Context.RequestAborted);

        if (usuario is null)
            return AuthenticateResult.Fail("Usuário indisponível.");

        var claims = new List<Claim>
        {
            new("sub", usuario.Id.ToString()),
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Login),
            new(UsuarioAtualHttp.ClaimEmpresa, sessao.EmpresaId.ToString()),
            new("sid", sessao.Id.ToString())
        };

        if (sessao.FilialId is Guid filialId)
            claims.Add(new Claim(UsuarioAtualHttp.ClaimFilial, filialId.ToString()));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}
