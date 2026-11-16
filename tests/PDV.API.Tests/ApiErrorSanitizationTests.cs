using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PDV.API.Middleware;
using PDV.Web.Autenticacao;
using Xunit;

namespace PDV.API.Tests;

public sealed class ApiErrorSanitizationTests
{
    [Theory]
    [InlineData(true, false, 0, EstadoFiliaisEstoque.Estado.Carregando)]
    [InlineData(false, true, 0, EstadoFiliaisEstoque.Estado.Falha)]
    [InlineData(false, false, 0, EstadoFiliaisEstoque.Estado.SemAcesso)]
    [InlineData(false, false, 1, EstadoFiliaisEstoque.Estado.Disponivel)]
    public void EmptyFilialListDeniesAccessOnlyAfterSuccessfulLoad(
        bool carregando,
        bool falha,
        int quantidadeFiliais,
        EstadoFiliaisEstoque.Estado esperado)
    {
        Assert.Equal(esperado,
            EstadoFiliaisEstoque.Avaliar(carregando, falha, quantidadeFiliais));
    }

    [Fact]
    public async Task HttpClientDoesNotExposeErrorResponseBodyToUi()
    {
        const string sentinel = "synthetic-secret-stack";
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent(
                $"Developer Exception Page: {sentinel}\r\nAuthorization: Bearer {sentinel}")
        };
        var httpClient = new HttpClient(new FixedResponseHandler(response))
        {
            BaseAddress = new Uri("https://api.test/")
        };
        var api = new EstoqueApiClient(
            new FixedHttpClientFactory(httpClient),
            new FixedAuthenticationStateProvider());

        var erro = await Assert.ThrowsAsync<HttpRequestException>(
            () => api.ListarFiliaisAsync());

        Assert.DoesNotContain(sentinel, erro.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("Authorization", erro.Message, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Developer Exception Page", erro.Message, StringComparison.Ordinal);
        Assert.Equal(HttpStatusCode.InternalServerError, erro.StatusCode);
    }

    [Fact]
    public async Task Api500ResponseIsSanitizedAndDoesNotLogRequestSecrets()
    {
        const string sentinel = "synthetic-bearer-secret";
        var logger = new CapturingLogger<ApiExceptionHandlingMiddleware>();
        var middleware = new ApiExceptionHandlingMiddleware(
            _ => throw new InvalidOperationException($"Synthetic stack detail {sentinel}"),
            logger);
        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization = $"Bearer {sentinel}";
        context.Response.Body = new MemoryStream();

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/problem+json; charset=utf-8", context.Response.ContentType);
        context.Response.Body.Position = 0;
        var body = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
        Assert.Contains("Ocorreu um erro inesperado.", body, StringComparison.Ordinal);
        Assert.DoesNotContain(sentinel, body, StringComparison.Ordinal);
        Assert.DoesNotContain("Synthetic stack detail", body, StringComparison.Ordinal);
        Assert.DoesNotContain("Authorization", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(sentinel, string.Join(" ", logger.Messages), StringComparison.Ordinal);
    }

    private sealed class FixedResponseHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(response);
    }

    private sealed class FixedHttpClientFactory(HttpClient client) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => client;
    }

    private sealed class FixedAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(
                [new Claim("api_access_token", "synthetic-test-token")],
                authenticationType: "test");
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }

    private sealed class CapturingLogger<T> : ILogger<T>
    {
        public List<string> Messages { get; } = [];

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
            => Messages.Add(formatter(state, exception));
    }
}
