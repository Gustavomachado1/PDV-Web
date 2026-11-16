using Microsoft.EntityFrameworkCore;
using PDV.API.Servicos;
using PDV.Application;
using PDV.Domain;
using PDV.Infrastructure;

namespace PDV.API.Endpoints;

public static class CatalogoEndpoints
{
    private const string PermissaoGerenciar = "catalogo.gerenciar";

    public static IEndpointRouteBuilder MapCatalogoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var grupo = endpoints.MapGroup("/api/catalogo")
            .RequireAuthorization()
            .AddEndpointFilter(async (context, next) =>
            {
                var usuarioAtual = context.HttpContext.RequestServices.GetRequiredService<IUsuarioAtual>();
                var autorizacao = context.HttpContext.RequestServices.GetRequiredService<AutorizacaoPermissaoService>();
                if (usuarioAtual.UsuarioId is not Guid usuarioId ||
                    !await autorizacao.TemPermissaoAsync(
                        usuarioId, PermissaoGerenciar, context.HttpContext.RequestAborted))
                    return Results.Forbid();

                return await next(context);
            });

        grupo.MapGet("/departamentos", async (PdvDbContext db, CancellationToken ct) =>
        {
            var itens = await db.Departamentos.AsNoTracking()
                .OrderBy(x => x.Nome)
                .Select(x => new DepartamentoDto(x.Id, x.Nome, x.Segmento))
                .ToListAsync(ct);
            return Results.Ok(itens);
        });

        grupo.MapPost("/departamentos", async (
            DepartamentoRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) ||
                request.Segmento?.Length > 100)
                return Erro("nome", "Informe um nome e um segmento com até 100 caracteres.");

            var departamento = new Departamento
            {
                Nome = request.Nome!.Trim(),
                Segmento = NormalizarOpcional(request.Segmento)
            };
            db.Departamentos.Add(departamento);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/api/catalogo/departamentos/{departamento.Id}",
                new DepartamentoDto(departamento.Id, departamento.Nome, departamento.Segmento));
        });

        grupo.MapPut("/departamentos/{id:guid}", async (
            Guid id, DepartamentoRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) ||
                request.Segmento?.Length > 100)
                return Erro("nome", "Informe um nome e um segmento com até 100 caracteres.");

            var departamento = await db.Departamentos.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (departamento is null)
                return Results.NotFound();

            departamento.Nome = request.Nome!.Trim();
            departamento.Segmento = NormalizarOpcional(request.Segmento);
            await db.SaveChangesAsync(ct);
            return Results.Ok(new DepartamentoDto(departamento.Id, departamento.Nome, departamento.Segmento));
        });

        grupo.MapGet("/categorias", async (PdvDbContext db, CancellationToken ct) =>
        {
            var itens = await db.Categorias.AsNoTracking()
                .Include(x => x.Departamento)
                .OrderBy(x => x.Nome)
                .ToListAsync(ct);
            return Results.Ok(itens.Select(x =>
                new CategoriaDto(x.Id, x.Nome, x.DepartamentoId, x.Departamento!.Nome)));
        });

        grupo.MapPost("/categorias", async (
            CategoriaRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) || request.DepartamentoId == Guid.Empty)
                return Erro("nome", "Informe o nome e o departamento da categoria.");

            if (!await db.Departamentos.AnyAsync(x => x.Id == request.DepartamentoId, ct))
                return Erro("departamentoId", "O departamento selecionado não foi encontrado.");

            var categoria = new Categoria
            {
                Nome = request.Nome!.Trim(),
                DepartamentoId = request.DepartamentoId
            };
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync(ct);
            var departamentoNome = await db.Departamentos
                .Where(x => x.Id == categoria.DepartamentoId)
                .Select(x => x.Nome)
                .SingleAsync(ct);
            return Results.Created($"/api/catalogo/categorias/{categoria.Id}",
                new CategoriaDto(categoria.Id, categoria.Nome, categoria.DepartamentoId, departamentoNome));
        });

        grupo.MapPut("/categorias/{id:guid}", async (
            Guid id, CategoriaRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) || request.DepartamentoId == Guid.Empty)
                return Erro("nome", "Informe o nome e o departamento da categoria.");

            if (!await db.Departamentos.AnyAsync(x => x.Id == request.DepartamentoId, ct))
                return Erro("departamentoId", "O departamento selecionado não foi encontrado.");

            var categoria = await db.Categorias.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (categoria is null)
                return Results.NotFound();

            categoria.Nome = request.Nome!.Trim();
            categoria.DepartamentoId = request.DepartamentoId;
            await db.SaveChangesAsync(ct);
            var departamentoNome = await db.Departamentos
                .Where(x => x.Id == categoria.DepartamentoId)
                .Select(x => x.Nome)
                .SingleAsync(ct);
            return Results.Ok(new CategoriaDto(categoria.Id, categoria.Nome, categoria.DepartamentoId, departamentoNome));
        });

        grupo.MapDelete("/categorias/{id:guid}", async (
            Guid id, PdvDbContext db, CancellationToken ct) =>
        {
            var categoria = await db.Categorias.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (categoria is null)
                return Results.NotFound();
            if (await db.Subcategorias.AnyAsync(x => x.CategoriaId == id, ct))
                return Results.Conflict(new { detail = "Mova as subcategorias para outra categoria da mesma empresa antes de excluir." });

            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        });

        grupo.MapGet("/subcategorias", async (PdvDbContext db, CancellationToken ct) =>
        {
            var itens = await db.Subcategorias.AsNoTracking()
                .Include(x => x.Categoria)
                    .ThenInclude(x => x!.Departamento)
                .OrderBy(x => x.Nome)
                .ToListAsync(ct);
            return Results.Ok(itens.Select(x => new SubcategoriaDto(
                x.Id,
                x.Nome,
                x.CategoriaId,
                x.Categoria!.Nome,
                x.Categoria.DepartamentoId,
                x.Categoria.Departamento!.Nome)));
        });

        grupo.MapPost("/subcategorias", async (
            SubcategoriaRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) || request.CategoriaId == Guid.Empty)
                return Erro("nome", "Informe o nome e a categoria da subcategoria.");
            if (!await db.Categorias.AnyAsync(x => x.Id == request.CategoriaId, ct))
                return Erro("categoriaId", "A categoria selecionada não foi encontrada.");

            var subcategoria = new Subcategoria
            {
                Nome = request.Nome!.Trim(),
                CategoriaId = request.CategoriaId
            };
            db.Subcategorias.Add(subcategoria);
            await db.SaveChangesAsync(ct);
            return Results.Created($"/api/catalogo/subcategorias/{subcategoria.Id}",
                await CarregarSubcategoriaAsync(db, subcategoria.Id, ct));
        });

        grupo.MapPut("/subcategorias/{id:guid}", async (
            Guid id, SubcategoriaRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            if (request is null || !NomeValido(request.Nome, 100) || request.CategoriaId == Guid.Empty)
                return Erro("nome", "Informe o nome e a categoria da subcategoria.");
            if (!await db.Categorias.AnyAsync(x => x.Id == request.CategoriaId, ct))
                return Erro("categoriaId", "A categoria selecionada não foi encontrada.");

            var subcategoria = await db.Subcategorias.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (subcategoria is null)
                return Results.NotFound();
            subcategoria.Nome = request.Nome!.Trim();
            subcategoria.CategoriaId = request.CategoriaId;
            await db.SaveChangesAsync(ct);
            return Results.Ok(await CarregarSubcategoriaAsync(db, subcategoria.Id, ct));
        });

        grupo.MapDelete("/subcategorias/{id:guid}", async (
            Guid id, PdvDbContext db, CancellationToken ct) =>
        {
            var subcategoria = await db.Subcategorias.SingleOrDefaultAsync(x => x.Id == id, ct);
            if (subcategoria is null)
                return Results.NotFound();
            if (await db.Produtos.AnyAsync(x => x.SubcategoriaId == id, ct))
                return Results.Conflict(new { detail = "Mova os produtos para outra subcategoria da mesma empresa antes de excluir." });

            db.Subcategorias.Remove(subcategoria);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        });

        grupo.MapGet("/produtos", async (
            string? busca, PdvDbContext db, CancellationToken ct) =>
        {
            var consulta = db.Produtos.AsNoTracking()
                .Include(x => x.Subcategoria)
                    .ThenInclude(x => x!.Categoria)
                        .ThenInclude(x => x!.Departamento)
                .Include(x => x.CodigosBarras)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                var termo = busca.Trim();
                consulta = consulta.Where(x => x.Descricao.Contains(termo) ||
                    x.CodigosBarras.Any(c => c.Ativo && c.Codigo.Contains(termo)));
            }

            var itens = await consulta.OrderBy(x => x.Descricao).ToListAsync(ct);
            return Results.Ok(itens.Select(x => new ProdutoDto(
                x.Id,
                x.Descricao,
                x.SubcategoriaId,
                x.Subcategoria!.Nome,
                x.Subcategoria.Categoria!.Nome,
                x.Subcategoria.Categoria.Departamento!.Nome,
                x.UnidadeMedida,
                x.Status,
                x.UsoInterno,
                x.CodigosBarras.Where(c => c.Ativo).OrderBy(c => c.Codigo).Select(c => c.Codigo).ToArray())));
        });

        grupo.MapPost("/produtos", async (
            ProdutoRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            var erro = ValidarProduto(request);
            if (erro is not null)
                return erro;

            var dados = request!;
            var codigos = NormalizarCodigos(dados.CodigosBarras);
            if (codigos.Length > 0 && await ExisteCodigoDuplicadoAsync(db, codigos, null, ct))
                return Erro("codigosBarras", "Um ou mais códigos de barras já estão cadastrados nesta empresa.");

            if (!await db.Subcategorias.AnyAsync(x => x.Id == dados.SubcategoriaId, ct))
                return Erro("subcategoriaId", "A subcategoria selecionada não foi encontrada.");

            var produto = new Produto
            {
                Descricao = dados.Descricao!.Trim(),
                SubcategoriaId = dados.SubcategoriaId,
                UnidadeMedida = dados.UnidadeMedida!.Trim(),
                Status = dados.Status,
                UsoInterno = dados.UsoInterno,
                CodigosBarras = codigos.Select(c => new CodigoBarrasProduto { Codigo = c }).ToArray()
            };
            db.Produtos.Add(produto);
            await db.SaveChangesAsync(ct);
            var resultado = await CarregarProdutoAsync(db, produto.Id, ct);
            return Results.Created($"/api/catalogo/produtos/{produto.Id}", resultado);
        });

        grupo.MapPut("/produtos/{id:guid}", async (
            Guid id, ProdutoRequest? request, PdvDbContext db, CancellationToken ct) =>
        {
            var erro = ValidarProduto(request, validarUnidadeMedida: false);
            if (erro is not null)
                return erro;

            var dados = request!;
            if (!await db.Subcategorias.AnyAsync(x => x.Id == dados.SubcategoriaId, ct))
                return Erro("subcategoriaId", "A subcategoria selecionada não foi encontrada.");

            var produto = await db.Produtos
                .Include(x => x.CodigosBarras)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
            if (produto is null)
                return Results.NotFound();

            if (!UnidadesMedidaProduto.PodeAtualizar(dados.UnidadeMedida, produto.UnidadeMedida))
                return Erro("unidadeMedida", "Selecione uma unidade de medida válida.");

            var codigos = NormalizarCodigos(dados.CodigosBarras);
            if (codigos.Length > 0 && await ExisteCodigoDuplicadoAsync(db, codigos, id, ct))
                return Erro("codigosBarras", "Um ou mais códigos de barras já foram cadastrados nesta empresa.");

            produto.Descricao = dados.Descricao!.Trim();
            produto.SubcategoriaId = dados.SubcategoriaId;
            produto.UnidadeMedida = dados.UnidadeMedida!;
            produto.Status = dados.Status;
            produto.UsoInterno = dados.UsoInterno;
            var codigosSolicitados = new HashSet<string>(codigos, StringComparer.OrdinalIgnoreCase);
            foreach (var codigoExistente in produto.CodigosBarras.Where(x => x.Ativo))
            {
                if (!codigosSolicitados.Contains(codigoExistente.Codigo))
                    codigoExistente.Ativo = false;
            }

            var codigosAtuais = new HashSet<string>(
                produto.CodigosBarras.Where(x => x.Ativo).Select(x => x.Codigo),
                StringComparer.OrdinalIgnoreCase);
            foreach (var codigo in codigos.Where(x => !codigosAtuais.Contains(x)))
            {
                produto.CodigosBarras.Add(new CodigoBarrasProduto
                {
                    ProdutoId = produto.Id,
                    Codigo = codigo
                });
            }
            await db.SaveChangesAsync(ct);
            return Results.Ok(await CarregarProdutoAsync(db, produto.Id, ct));
        });

        return endpoints;
    }

    private static IResult? ValidarProduto(ProdutoRequest? request, bool validarUnidadeMedida = true)
    {
        if (request is null)
            return Erro("produto", "Informe os dados do produto.");
        if (!NomeValido(request.Descricao, 200))
            return Erro("descricao", "Informe uma descrição com até 200 caracteres.");
        if (request.SubcategoriaId == Guid.Empty)
            return Erro("subcategoriaId", "Selecione uma subcategoria.");
        if (validarUnidadeMedida && !UnidadesMedidaProduto.EhValida(request.UnidadeMedida))
            return Erro("unidadeMedida", "Selecione uma unidade de medida válida.");
        if (!Enum.IsDefined(request.Status))
            return Erro("status", "Selecione uma situação válida.");

        var codigos = NormalizarCodigos(request.CodigosBarras);
        if (codigos.Any(c => c.Length > 100))
            return Erro("codigosBarras", "Cada código de barras deve ter até 100 caracteres.");
        if (codigos.Distinct(StringComparer.OrdinalIgnoreCase).Count() != codigos.Length)
            return Erro("codigosBarras", "O produto não pode repetir um código de barras.");

        return null;
    }

    private static async Task<bool> ExisteCodigoDuplicadoAsync(
        PdvDbContext db, string[] codigos, Guid? produtoId, CancellationToken cancellationToken)
        => await db.CodigosBarrasProdutos.AnyAsync(x =>
            codigos.Contains(x.Codigo) && (produtoId == null || x.ProdutoId != produtoId),
            cancellationToken);

    private static async Task<ProdutoDto> CarregarProdutoAsync(
        PdvDbContext db, Guid id, CancellationToken cancellationToken)
    {
        var produto = await db.Produtos.AsNoTracking()
            .Include(x => x.Subcategoria)
                .ThenInclude(x => x!.Categoria)
                    .ThenInclude(x => x!.Departamento)
            .Include(x => x.CodigosBarras)
            .SingleAsync(x => x.Id == id, cancellationToken);
        return new ProdutoDto(
            produto.Id,
            produto.Descricao,
            produto.SubcategoriaId,
            produto.Subcategoria!.Nome,
            produto.Subcategoria.Categoria!.Nome,
            produto.Subcategoria.Categoria.Departamento!.Nome,
            produto.UnidadeMedida,
            produto.Status,
            produto.UsoInterno,
            produto.CodigosBarras.Where(c => c.Ativo).OrderBy(c => c.Codigo).Select(c => c.Codigo).ToArray());
    }

    private static async Task<SubcategoriaDto> CarregarSubcategoriaAsync(
        PdvDbContext db, Guid id, CancellationToken cancellationToken)
    {
        var subcategoria = await db.Subcategorias.AsNoTracking()
            .Include(x => x.Categoria)
                .ThenInclude(x => x!.Departamento)
            .SingleAsync(x => x.Id == id, cancellationToken);
        return new SubcategoriaDto(
            subcategoria.Id,
            subcategoria.Nome,
            subcategoria.CategoriaId,
            subcategoria.Categoria!.Nome,
            subcategoria.Categoria.DepartamentoId,
            subcategoria.Categoria.Departamento!.Nome);
    }

    private static string[] NormalizarCodigos(string?[]? codigos)
        => codigos?
            .Select(c => c?.Trim())
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Select(c => c!)
            .ToArray() ?? [];

    private static bool NomeValido(string? valor, int tamanhoMaximo)
        => !string.IsNullOrWhiteSpace(valor) && valor.Trim().Length <= tamanhoMaximo;

    private static string? NormalizarOpcional(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    private static IResult Erro(string campo, string mensagem)
        => Results.ValidationProblem(new Dictionary<string, string[]> { [campo] = [mensagem] });

    public sealed record DepartamentoRequest(string? Nome, string? Segmento);
    public sealed record DepartamentoDto(Guid Id, string Nome, string? Segmento);
    public sealed record CategoriaRequest(string? Nome, Guid DepartamentoId);
    public sealed record CategoriaDto(Guid Id, string Nome, Guid DepartamentoId, string DepartamentoNome);
    public sealed record SubcategoriaRequest(string? Nome, Guid CategoriaId);
    public sealed record SubcategoriaDto(
        Guid Id, string Nome, Guid CategoriaId, string CategoriaNome, Guid DepartamentoId, string DepartamentoNome);
    public sealed record ProdutoRequest(
        string? Descricao,
        Guid SubcategoriaId,
        string? UnidadeMedida,
        string?[]? CodigosBarras,
        StatusProduto Status,
        bool UsoInterno);
    public sealed record ProdutoDto(
        Guid Id,
        string Descricao,
        Guid SubcategoriaId,
        string SubcategoriaNome,
        string CategoriaNome,
        string DepartamentoNome,
        string UnidadeMedida,
        StatusProduto Status,
        bool UsoInterno,
        string[] CodigosBarras);
}
