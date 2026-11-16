using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PDV.API.Hubs;
using PDV.API.Endpoints;
using PDV.API.Middleware;
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
builder.Services.AddScoped<AutorizacaoPermissaoService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<ProvisionamentoInicialService>();
builder.Services.AddScoped<ProvisionamentoAssinaturaLocalService>();
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
app.UseMiddleware<ApiExceptionHandlingMiddleware>();

var solicitouBootstrap = args.Contains("--bootstrap", StringComparer.Ordinal);
var solicitouAssinaturaLocal = args.Contains("--provisionar-assinatura-local", StringComparer.Ordinal);
var solicitouSeedCatalogo = args.Contains("--seed-catalogo-padrao", StringComparer.Ordinal);
if (new[] { solicitouBootstrap, solicitouAssinaturaLocal, solicitouSeedCatalogo }.Count(x => x) > 1)
    throw new InvalidOperationException("Execute apenas uma operação administrativa por vez.");

if (solicitouAssinaturaLocal)
{
    if (!app.Environment.IsDevelopment())
        throw new InvalidOperationException(
            "A recuperação da assinatura só pode ser executada com ASPNETCORE_ENVIRONMENT=Development.");

    await using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider.GetRequiredService<ProvisionamentoAssinaturaLocalService>()
        .ExecutarAsync(
            args.Contains("--confirmar-banco-local", StringComparer.Ordinal),
            app.Lifetime.ApplicationStopping);
    Console.WriteLine("Assinatura local Essencial provisionada sem cobrança.");
    return;
}

if (solicitouSeedCatalogo)
{
    if (!args.Contains("--confirmar-catalogo-padrao", StringComparer.Ordinal))
        throw new InvalidOperationException(
            "Confirme a população do catálogo com --confirmar-catalogo-padrao.");

    var valorEmpresaId = Environment.GetEnvironmentVariable("PDV_CATALOGO_EMPRESA_ID");
    if (!Guid.TryParse(valorEmpresaId, out var empresaId) || empresaId == Guid.Empty)
        throw new InvalidOperationException(
            "Defina PDV_CATALOGO_EMPRESA_ID com o identificador da empresa.");
    var segmento = Environment.GetEnvironmentVariable("PDV_CATALOGO_SEGMENTO");

    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<PdvDbContext>();
    var empresaExiste = await db.Empresas.IgnoreQueryFilters()
        .AnyAsync(x => x.Id == empresaId, app.Lifetime.ApplicationStopping);
    if (!empresaExiste)
        throw new InvalidOperationException("A empresa informada não foi encontrada no banco configurado.");

    scope.ServiceProvider.GetRequiredService<EmpresaContextoLogin>().Definir(empresaId);
    var resultado = await scope.ServiceProvider.GetRequiredService<CatalogoPadraoSeeder>()
        .PopularAsync(empresaId, segmento, app.Lifetime.ApplicationStopping);
    Console.WriteLine(
        $"Catálogo padrão processado: {resultado.Departamentos} departamentos, " +
        $"{resultado.Categorias} categorias e {resultado.Subcategorias} subcategorias adicionados.");
    return;
}

if (solicitouBootstrap)
{
    var configuracao = ProvisionamentoInicialConfiguracao.CarregarDoAmbiente();
    await using var scope = app.Services.CreateAsyncScope();
    await scope.ServiceProvider.GetRequiredService<ProvisionamentoInicialService>()
        .ExecutarAsync(configuracao, app.Lifetime.ApplicationStopping);
    Console.WriteLine("Provisionamento inicial concluído.");
    return;
}

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
app.MapCatalogoEndpoints();
app.MapEstoqueEndpoints();

app.Run();
