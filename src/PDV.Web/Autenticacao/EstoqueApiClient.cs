using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using PDV.Domain;

namespace PDV.Web.Autenticacao;

public sealed class EstoqueApiClient(
    IHttpClientFactory clientes,
    AuthenticationStateProvider estadoAutenticacao)
{
    public Task<IReadOnlyList<FilialDto>> ListarFiliaisAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<FilialDto>>("api/estoque/filiais", ct);

    public Task<IReadOnlyList<ProdutoDto>> ListarProdutosAsync(Guid filialId, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<ProdutoDto>>($"api/estoque/{filialId}/produtos", ct);

    public Task<ConfiguracaoEstoqueNegativoDto> ObterConfiguracaoEstoqueNegativoAsync(
        Guid filialId, CancellationToken ct = default)
        => GetAsync<ConfiguracaoEstoqueNegativoDto>(
            $"api/estoque/{filialId}/configuracao-negativo", ct);

    public Task<IReadOnlyList<MovimentoDto>> ListarMovimentosAsync(Guid filialId, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<MovimentoDto>>($"api/estoque/{filialId}/movimentos", ct);

    public Task<IReadOnlyList<MovimentoPendenteDto>> ListarPendentesAsync(
        Guid filialId, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<MovimentoPendenteDto>>($"api/estoque/{filialId}/pendentes", ct);

    public Task<IReadOnlyList<InventarioDto>> ListarInventariosAsync(
        Guid filialId, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<InventarioDto>>($"api/estoque/{filialId}/inventarios", ct);

    public Task SolicitarMovimentoAsync(
        Guid filialId, SolicitarMovimentoRequest request, CancellationToken ct = default)
        => SendWithoutResponseAsync(HttpMethod.Post, $"api/estoque/{filialId}/movimentos", request, ct);

    public Task DecidirMovimentoAsync(
        Guid filialId, Guid movimentoId, DecidirMovimentoRequest request, CancellationToken ct = default)
        => SendWithoutResponseAsync(
            HttpMethod.Post, $"api/estoque/{filialId}/movimentos/{movimentoId}/decisao", request, ct);

    public Task ConfigurarAsync(
        Guid filialId, Guid produtoId, ConfiguracaoRequest request, CancellationToken ct = default)
        => SendWithoutResponseAsync(
            HttpMethod.Put, $"api/estoque/{filialId}/produtos/{produtoId}/configuracao", request, ct);

    public Task ConfigurarEstoqueNegativoEmpresaAsync(
        Guid filialId, bool permiteEstoqueNegativo, CancellationToken ct = default)
        => SendWithoutResponseAsync(
            HttpMethod.Put, $"api/estoque/{filialId}/configuracao-negativo/empresa",
            new ConfigurarEstoqueNegativoEmpresaRequest(permiteEstoqueNegativo), ct);

    public Task ConfigurarEstoqueNegativoFilialAsync(
        Guid filialId, bool? permiteEstoqueNegativo, CancellationToken ct = default)
        => SendWithoutResponseAsync(
            HttpMethod.Put, $"api/estoque/{filialId}/configuracao-negativo/filial",
            new ConfigurarEstoqueNegativoFilialRequest(permiteEstoqueNegativo), ct);

    public Task<InventarioConcluidoDto> ConcluirInventarioAsync(
        Guid filialId, ConcluirInventarioRequest request, CancellationToken ct = default)
        => SendAsync<InventarioConcluidoDto>(
            HttpMethod.Post, $"api/estoque/{filialId}/inventarios", request, ct);

    private async Task<T> GetAsync<T>(string caminho, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(HttpMethod.Get, caminho, null, cancellationToken);
        using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
        ValidarResposta(resposta);
        return await resposta.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API retornou uma resposta vazia para o estoque.");
    }

    private async Task<T> SendAsync<T>(
        HttpMethod metodo, string caminho, object corpo, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(metodo, caminho, corpo, cancellationToken);
        using var resposta = await clientes.CreateClient("PdvApi").SendAsync(requisicao, cancellationToken);
        ValidarResposta(resposta);
        return await resposta.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API retornou uma resposta vazia para o estoque.");
    }

    private async Task SendWithoutResponseAsync(
        HttpMethod metodo, string caminho, object corpo, CancellationToken cancellationToken)
    {
        using var requisicao = await CriarRequisicaoAsync(metodo, caminho, corpo, cancellationToken);
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
            throw new UnauthorizedAccessException(
                "A sessão expirou ou seu usuário não tem permissão para esta operação.");
        if (resposta.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.NotFound or HttpStatusCode.Conflict)
            throw new InvalidOperationException(MensagemErroHttp.Para(resposta.StatusCode));

        throw new HttpRequestException(
            MensagemErroHttp.Para(resposta.StatusCode),
            null,
            resposta.StatusCode);
    }

    public sealed record FilialDto(Guid Id, string Codigo, string Nome);
    public sealed record ProdutoDto(
        Guid ProdutoId, string Descricao, string UnidadeMedida, StatusProduto Status,
        decimal Quantidade, decimal? EstoqueMinimo, decimal? EstoqueIdeal, bool Disponivel, bool EstoqueBaixo);
    public sealed record ConfiguracaoEstoqueNegativoDto(
        bool PermiteEstoqueNegativoEmpresa,
        bool? PermiteEstoqueNegativoFilial,
        bool PermiteEstoqueNegativoEfetivo);
    public sealed record ConfigurarEstoqueNegativoEmpresaRequest(bool PermiteEstoqueNegativo);
    public sealed record ConfigurarEstoqueNegativoFilialRequest(bool? PermiteEstoqueNegativo);
    public sealed record MovimentoDto(
        Guid Id, Guid ProdutoId, string Produto, TipoMovimentoEstoque Tipo, decimal Quantidade,
        decimal SaldoAnterior, decimal SaldoResultante, Guid SolicitanteId, string SolicitanteNome,
        Guid? AutorizadorId, string? AutorizadorNome,
        string? Justificativa, DateTime ConcluidoEmUtc);
    public sealed record MovimentoPendenteDto(
        Guid Id, Guid ProdutoId, string Produto, TipoMovimentoEstoque Tipo, decimal Quantidade,
        Guid SolicitanteId, string SolicitanteNome, string? Justificativa, DateTime SolicitadoEmUtc);
    public sealed record SolicitarMovimentoRequest(
        Guid ProdutoId, TipoMovimentoEstoque Tipo, decimal Quantidade, string? Justificativa);
    public sealed record DecidirMovimentoRequest(bool Aprovar, string Login, string Senha);
    public sealed record ConfiguracaoRequest(decimal? EstoqueMinimo, decimal? EstoqueIdeal, bool Disponivel);
    public sealed record ItemContagemRequest(Guid ProdutoId, decimal QuantidadeContada);
    public sealed record ConcluirInventarioRequest(
        TipoInventarioEstoque Tipo, IReadOnlyList<ItemContagemRequest> Itens, string? Justificativa);
    public sealed record InventarioConcluidoDto(
        Guid Id, TipoInventarioEstoque Tipo, DateTime ConcluidoEmUtc, int ItensContados, int ItensAjustados);
    public sealed record InventarioDto(
        Guid Id, TipoInventarioEstoque Tipo, Guid UsuarioId, string UsuarioNome, DateTime ConcluidoEmUtc,
        int ItensContados, int ItensAjustados, string? Justificativa);
}
