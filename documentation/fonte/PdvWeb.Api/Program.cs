using PdvWeb.Api.Hubs;
using PdvWeb.Api.Servicos;
using PdvWeb.Application;
using PdvWeb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioAtual, UsuarioAtualHttp>();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("PdvWeb")
    ?? throw new InvalidOperationException("Connection string 'PdvWeb' não configurada."));

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

app.MapGet("/saude", () => Results.Ok(new { status = "ok" }));
app.MapHub<NotificacoesHub>("/hubs/notificacoes");

app.Run();
