using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using PDV.API.Hubs;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<EmpresaContextoLogin>();
builder.Services.AddScoped<IUsuarioAtual, UsuarioAtualHttp>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
builder.Services.AddScoped<AutenticacaoService>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("PdvWeb")
    ?? throw new InvalidOperationException("Connection string 'PdvWeb' não configurada."));

const string esquemaSessao = "Sessao";
builder.Services.AddAuthentication(esquemaSessao)
    .AddScheme<AuthenticationSchemeOptions, SessaoAuthenticationHandler>(esquemaSessao, _ => { });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/saude", () => Results.Ok(new { status = "ok" }));
app.MapPost("/auth/login", async (
    LoginRequest? request, HttpContext context, AutenticacaoService autenticacao, CancellationToken cancellationToken) =>
{
    if (request is null)
        return Results.BadRequest();

    context.Response.Headers.CacheControl = "no-store";
    context.Response.Headers.Pragma = "no-cache";
    var sessao = await autenticacao.EntrarAsync(
        request, context.Connection.RemoteIpAddress?.ToString(), cancellationToken);
    if (sessao is null)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        accessToken = sessao.AccessToken,
        tokenType = "Bearer",
        expiresAtUtc = sessao.ExpiraEmUtc
    });
}).AllowAnonymous();

app.MapPost("/auth/logout", async (
    ClaimsPrincipal usuario, AutenticacaoService autenticacao, CancellationToken cancellationToken) =>
{
    var valor = usuario.FindFirstValue("sid");
    if (!Guid.TryParse(valor, out var sessaoId))
        return Results.Unauthorized();

    await autenticacao.EncerrarAsync(sessaoId, cancellationToken);
    return Results.NoContent();
}).RequireAuthorization();

app.MapGet("/auth/me", (ClaimsPrincipal usuario) => Results.Ok(new
{
    usuarioId = usuario.FindFirstValue(UsuarioAtualHttp.ClaimUsuario),
    empresaId = usuario.FindFirstValue(UsuarioAtualHttp.ClaimEmpresa),
    filialId = usuario.FindFirstValue(UsuarioAtualHttp.ClaimFilial)
})).RequireAuthorization();

app.MapHub<NotificacoesHub>("/hubs/notificacoes").RequireAuthorization();

app.Run();
