using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace PDV.Web.Autenticacao;

public sealed class CatalogoApiClient(
    IHttpClientFactory clientes,
    AuthenticationStateProvider estadoAutenticacao)
{
    public Task<IReadOnlyList<DepartamentoDto>> ListarDepartamentosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<DepartamentoDto>>("api/catalogo/departamentos", ct);

    public Task<IReadOnlyList<CategoriaDto>> ListarCategoriasAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<CategoriaDto>>("api/catalogo/categorias", ct);

    public Task<IReadOnlyList<SubcategoriaDto>> ListarSubcategoriasAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<SubcategoriaDto>>("api/catalogo/subcategorias", ct);

    public Task<IReadOnlyList<ProdutoDto>> ListarProdutosAsync(string? busca, CancellationToken ct = default)
    {
        var caminho = "api/catalogo/produtos";
        if (!string.IsNullOrWhiteSpace(busca))
            caminho += $"?busca={Uri.EscapeDataString(busca.Trim())}";
        return GetAsync<IReadOnlyList<ProdutoDto>>(caminho, ct);
    }

    public Task<DepartamentoDto> CriarDepartamentoAsync(
        string nome, string? segmento, CancellationToken ct = default)
        => SendAsync<DepartamentoDto>(HttpMethod.Post, "api/catalogo/departamentos",
            new DepartamentoRequest(nome, segmento), ct);

    public Task<DepartamentoDto> AtualizarDepartamentoAsync(
        Guid id, string nome, string? segmento, CancellationToken ct = default)
        => SendAsync<DepartamentoDto>(HttpMethod.Put, $"api/catalogo/departamentos/{id}",
            new DepartamentoRequest(nome, segmento), ct);

    public Task<CategoriaDto> CriarCategoriaAsync(
        string nome, Guid departamentoId, CancellationToken ct = default)
        => SendAsync<CategoriaDto>(HttpMethod.Post, "api/catalogo/categorias",
            new CategoriaRequest(nome, departamentoId), ct);

    public Task<CategoriaDto> AtualizarCategoriaAsync(
        Guid id, string nome, Guid departamentoId, CancellationToken ct = default)
        => SendAsync<CategoriaDto>(HttpMethod.Put, $"api/catalogo/categorias/{id}",
            new CategoriaRequest(nome, departamentoId), ct);

    public Task RemoverCategoriaAsync(Guid id, CancellationToken ct = default)
        => DeleteAsync($"api/catalogo/categorias/{id}", ct);

    public Task<SubcategoriaDto> CriarSubcategoriaAsync(
        string nome, Guid categoriaId, CancellationToken ct = default)
        => SendAsync<SubcategoriaDto>(HttpMethod.Post, "api/catalogo/subcategorias",
            new SubcategoriaRequest(nome, categoriaId), ct);

    public Task<SubcategoriaDto> AtualizarSubcategoriaAsync(
        Guid id, string nome, Guid categoriaId, CancellationToken ct = default)
        => SendAsync<SubcategoriaDto>(HttpMethod.Put, $"api/catalogo/subcategorias/{id}",
            new SubcategoriaRequest(nome, categoriaId), ct);

    public Task RemoverSubcategoriaAsync(Guid id, CancellationToken ct = default)
        => DeleteAsync($"api/catalogo/subcategorias/{id}", ct);

    public Task<ProdutoDto> CriarProdutoAsync(ProdutoRequest produto, CancellationToken ct = default)
        => SendAsync<ProdutoDto>(HttpMethod.Post, "api/catalogo/produtos", produto, ct);

    public Task<ProdutoDto> AtualizarProdutoAsync(
        Guid id, ProdutoRequest produto, CancellationToken ct = default)
        => SendAsync<ProdutoDto>(HttpMethod.Put, $"api/catalogo/produtos/{id}", produto, ct);

    private async Task<T> GetAsync<T>(string caminho, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(HttpMethod.Get, caminho, null, cancellationToken);
        using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
        ValidarResposta(resposta);
        return await resposta.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API retornou uma resposta vazia para o catálogo.");
    }

    private async Task<T> SendAsync<T>(
        HttpMethod metodo, string caminho, object corpo, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(metodo, caminho, corpo, cancellationToken);
        using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
        ValidarResposta(resposta);
        return await resposta.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API retornou uma resposta vazia para o catálogo.");
    }

    private async Task DeleteAsync(string caminho, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(HttpMethod.Delete, caminho, null, cancellationToken);
        using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
        ValidarResposta(resposta);
    }

    private async Task<HttpRequestMessage> CriarRequisicaoAsync(
        HttpMethod metodo, string caminho, object? corpo, CancellationToken cancellationToken)
    {
        var estado = await estadoAutenticacao.GetAuthenticationStateAsync();
        var token = estado.User.FindFirst("api_access_token")?.Value;
        if (string.IsNullOrWhiteSpace(token))
            throw new UnauthorizedAccessException("A sessão da API não está disponível. Entre novamente.");

        var requisicao = new HttpRequestMessage(metodo, caminho);
        requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        if (corpo is not null)
            requisicao.Content = JsonContent.Create(corpo);
        return requisicao;
    }

    private static void ValidarResposta(HttpResponseMessage resposta)
    {
        if (resposta.IsSuccessStatusCode)
            return;

        if (resposta.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            throw new UnauthorizedAccessException("Seu usuário não tem permissão para gerenciar o catálogo.");
        if (resposta.StatusCode == HttpStatusCode.NotFound)
            throw new InvalidOperationException(MensagemErroHttp.Para(resposta.StatusCode));
        if (resposta.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.Conflict)
            throw new InvalidOperationException(MensagemErroHttp.Para(resposta.StatusCode));

        throw new HttpRequestException(
            MensagemErroHttp.Para(resposta.StatusCode),
            null,
            resposta.StatusCode);
    }

    public sealed record DepartamentoRequest(string Nome, string? Segmento);
    public sealed record CategoriaRequest(string Nome, Guid DepartamentoId);
    public sealed record SubcategoriaRequest(string Nome, Guid CategoriaId);
    public sealed record ProdutoRequest(
        string Descricao,
        Guid SubcategoriaId,
        string UnidadeMedida,
        string[] CodigosBarras,
        int Status,
        bool UsoInterno);
    public sealed record DepartamentoDto(Guid Id, string Nome, string? Segmento);
    public sealed record CategoriaDto(Guid Id, string Nome, Guid DepartamentoId, string DepartamentoNome);
    public sealed record SubcategoriaDto(
        Guid Id, string Nome, Guid CategoriaId, string CategoriaNome, Guid DepartamentoId, string DepartamentoNome);
    public sealed record ProdutoDto(
        Guid Id,
        string Descricao,
        Guid SubcategoriaId,
        string SubcategoriaNome,
        string CategoriaNome,
        string DepartamentoNome,
        string UnidadeMedida,
        int Status,
        bool UsoInterno,
        string[] CodigosBarras);
}
