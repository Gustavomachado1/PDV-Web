using Microsoft.EntityFrameworkCore;
using PDV.API.Servicos;

namespace PDV.API.Endpoints;

public static class EstoqueEndpoints
{
    public static IEndpointRouteBuilder MapEstoqueEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var grupo = endpoints.MapGroup("/api/estoque").RequireAuthorization();

        grupo.MapGet("/filiais", (EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () => Results.Ok(await estoque.ListarFiliaisAsync(ct))));

        grupo.MapGet("/{filialId:guid}/produtos", (
            Guid filialId, EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () => Results.Ok(await estoque.ListarProdutosAsync(filialId, ct))));

        grupo.MapPut("/{filialId:guid}/produtos/{produtoId:guid}/configuracao", (
            Guid filialId, Guid produtoId, ConfigurarEstoqueRequest? request,
            EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () =>
            {
                await estoque.AtualizarConfiguracaoAsync(filialId, produtoId, request, ct);
                return Results.NoContent();
            }));

        grupo.MapGet("/{filialId:guid}/movimentos", (
            Guid filialId, EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () => Results.Ok(await estoque.ListarMovimentosAsync(filialId, ct))));

        grupo.MapPost("/{filialId:guid}/movimentos", (
            Guid filialId, SolicitarMovimentoEstoqueRequest? request,
            EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () =>
            {
                var id = await estoque.SolicitarMovimentoAsync(filialId, request, ct);
                return Results.Accepted($"/api/estoque/{filialId}/pendentes", new { id });
            }));

        grupo.MapGet("/{filialId:guid}/pendentes", (
            Guid filialId, EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () => Results.Ok(await estoque.ListarPendentesAsync(filialId, ct))));

        grupo.MapPost("/{filialId:guid}/movimentos/{movimentoId:guid}/decisao", (
            Guid filialId, Guid movimentoId, DecidirMovimentoEstoqueRequest? request,
            EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () =>
            {
                await estoque.DecidirMovimentoAsync(filialId, movimentoId, request, ct);
                return Results.NoContent();
            }));

        grupo.MapPost("/{filialId:guid}/inventarios", (
            Guid filialId, ConcluirInventarioEstoqueRequest? request,
            EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () =>
            {
                var resultado = await estoque.ConcluirInventarioAsync(filialId, request, ct);
                return Results.Created($"/api/estoque/{filialId}/inventarios/{resultado.Id}", resultado);
            }));

        grupo.MapGet("/{filialId:guid}/inventarios", (
            Guid filialId, EstoqueService estoque, CancellationToken ct) =>
            ExecutarAsync(async () => Results.Ok(await estoque.ListarInventariosAsync(filialId, ct))));

        return endpoints;
    }

    private static async Task<IResult> ExecutarAsync(Func<Task<IResult>> acao)
    {
        try
        {
            return await acao();
        }
        catch (EstoqueException ex)
        {
            return ex.StatusCode switch
            {
                400 => Results.ValidationProblem(new Dictionary<string, string[]> { [ex.Campo] = [ex.Message] }),
                401 => Results.Unauthorized(),
                403 => Results.Forbid(),
                404 => Results.NotFound(new { detail = ex.Message }),
                409 => Results.Conflict(new { detail = ex.Message }),
                _ => Results.Problem(statusCode: ex.StatusCode, detail: ex.Message)
            };
        }
        catch (DbUpdateConcurrencyException)
        {
            return Results.Conflict(new { detail = "O estoque mudou durante a operação. Atualize os dados e tente novamente." });
        }
    }
}
