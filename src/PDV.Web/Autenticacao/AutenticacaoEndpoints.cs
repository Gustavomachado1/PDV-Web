using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace PDV.Web.Autenticacao;

public static class AutenticacaoEndpoints
{
    public static IEndpointRouteBuilder MapAutenticacaoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/account/login", EntrarAsync);
        endpoints.MapPost("/account/logout", SairAsync);
        return endpoints;
    }

    private static async Task<IResult> EntrarAsync(
        HttpContext context,
        [FromForm] FormularioLogin formulario,
        IHttpClientFactory clientes,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        context.Response.Headers.CacheControl = "no-store";
        if (string.IsNullOrWhiteSpace(formulario.CodigoEmpresa) ||
            string.IsNullOrWhiteSpace(formulario.Login) ||
            string.IsNullOrEmpty(formulario.Senha))
            return Results.LocalRedirect("/login?erro=credenciais");

        var api = clientes.CreateClient("PdvApi");
        try
        {
            using var respostaLogin = await api.PostAsJsonAsync("auth/login", new
            {
                codigoEmpresa = formulario.CodigoEmpresa,
                login = formulario.Login,
                senha = formulario.Senha,
                dispositivo = "PDV-Web"
            }, cancellationToken);

            if (respostaLogin.StatusCode == HttpStatusCode.Unauthorized ||
                respostaLogin.StatusCode == HttpStatusCode.BadRequest)
                return Results.LocalRedirect("/login?erro=credenciais");

            if (!respostaLogin.IsSuccessStatusCode)
            {
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogWarning("A API recusou a tentativa de login com status {StatusCode}.", respostaLogin.StatusCode);
                return Results.LocalRedirect("/login?erro=indisponivel");
            }

            var sessao = await respostaLogin.Content.ReadFromJsonAsync<SessaoApi>(
                cancellationToken: cancellationToken);
            if (sessao is null || !string.Equals(sessao.TokenType, "Bearer", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(sessao.AccessToken) || sessao.ExpiresAtUtc <= DateTimeOffset.UtcNow)
            {
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogError("A API retornou uma resposta de login inválida.");
                return Results.LocalRedirect("/login?erro=indisponivel");
            }

            using var requisicaoUsuario = new HttpRequestMessage(HttpMethod.Get, "auth/me");
            requisicaoUsuario.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessao.AccessToken);
            using var respostaUsuario = await api.SendAsync(requisicaoUsuario, cancellationToken);
            if (!respostaUsuario.IsSuccessStatusCode)
            {
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogWarning("A API não confirmou a sessão recém-criada; status {StatusCode}.", respostaUsuario.StatusCode);
                await RevogarSessaoAsync(api, sessao.AccessToken, loggerFactory, cancellationToken);
                return Results.LocalRedirect("/login?erro=indisponivel");
            }

            var usuario = await respostaUsuario.Content.ReadFromJsonAsync<UsuarioApi>(
                cancellationToken: cancellationToken);
            if (usuario is null || usuario.UsuarioId == Guid.Empty || usuario.EmpresaId == Guid.Empty)
            {
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogError("A API retornou uma identidade inválida para a sessão autenticada.");
                await RevogarSessaoAsync(api, sessao.AccessToken, loggerFactory, cancellationToken);
                return Results.LocalRedirect("/login?erro=indisponivel");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new(ClaimTypes.Name, formulario.Login.Trim()),
                new("empresa_id", usuario.EmpresaId.ToString()),
                new("api_access_token", sessao.AccessToken),
                new("sessao_expira_em", sessao.ExpiresAtUtc.ToUnixTimeSeconds().ToString())
            };
            if (usuario.FilialId is Guid filialId)
                claims.Add(new Claim("filial_id", filialId.ToString()));

            var principal = new ClaimsPrincipal(new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme));
            var propriedades = new AuthenticationProperties
            {
                ExpiresUtc = sessao.ExpiresAtUtc,
                IsPersistent = false,
                AllowRefresh = false
            };
            propriedades.StoreTokens(
            [
                new AuthenticationToken { Name = "access_token", Value = sessao.AccessToken }
            ]);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, propriedades);
            return Results.LocalRedirect("/");
        }
        catch (HttpRequestException ex)
        {
            loggerFactory.CreateLogger("AutenticacaoWeb").LogError(ex, "Não foi possível acessar a API de autenticação.");
            return Results.LocalRedirect("/login?erro=indisponivel");
        }
        catch (JsonException ex)
        {
            loggerFactory.CreateLogger("AutenticacaoWeb").LogError(ex, "A API retornou uma resposta de autenticação inválida.");
            return Results.LocalRedirect("/login?erro=indisponivel");
        }
    }

    private static async Task<IResult> SairAsync(
        HttpContext context,
        [FromForm] FormularioLogout formulario,
        IHttpClientFactory clientes,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        context.Response.Headers.CacheControl = "no-store";
        var autenticacao = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var token = autenticacao.Properties?.GetTokenValue("access_token");
        if (!string.IsNullOrWhiteSpace(token))
        {
            try
            {
                using var requisicao = new HttpRequestMessage(HttpMethod.Post, "auth/logout");
                requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
                if (!resposta.IsSuccessStatusCode && resposta.StatusCode != HttpStatusCode.Unauthorized)
                    loggerFactory.CreateLogger("AutenticacaoWeb")
                        .LogWarning("A API não confirmou o encerramento da sessão; status {StatusCode}.", resposta.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogError(ex, "Não foi possível revogar a sessão na API durante o logout.");
            }
        }

        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Results.LocalRedirect("/login?estado=saiu");
    }

    private static async Task RevogarSessaoAsync(
        HttpClient api,
        string token,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        try
        {
            using var requisicao = new HttpRequestMessage(HttpMethod.Post, "auth/logout");
            requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using var resposta = await api.SendAsync(requisicao, cancellationToken);
            if (!resposta.IsSuccessStatusCode && resposta.StatusCode != HttpStatusCode.Unauthorized)
                loggerFactory.CreateLogger("AutenticacaoWeb")
                    .LogWarning("A API não confirmou a revogação de uma sessão incompleta; status {StatusCode}.", resposta.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            loggerFactory.CreateLogger("AutenticacaoWeb")
                .LogError(ex, "Não foi possível revogar uma sessão incompleta na API.");
        }
    }

    public sealed class FormularioLogin
    {
        public string CodigoEmpresa { get; set; } = "";
        public string Login { get; set; } = "";
        public string Senha { get; set; } = "";
    }

    public sealed class FormularioLogout
    {
        public string? RequestVerificationToken { get; set; }
    }

    private sealed record SessaoApi(string AccessToken, string TokenType, DateTimeOffset ExpiresAtUtc);
    private sealed record UsuarioApi(Guid UsuarioId, Guid EmpresaId, Guid? FilialId);
}
