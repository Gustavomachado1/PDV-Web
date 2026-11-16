using System.Text.Json;

namespace PDV.API.Middleware;

public sealed class ApiExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ApiExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(
                "Unhandled API exception {ExceptionType} at {StackTrace}; trace {TraceId}",
                exception.GetType().FullName,
                exception.StackTrace,
                context.TraceIdentifier);

            if (context.Response.HasStarted)
            {
                context.Abort();
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json; charset=utf-8";
            context.Response.Headers.CacheControl = "no-store";
            var corpo = JsonSerializer.Serialize(
                new
                {
                    title = "Erro interno do servidor",
                    status = StatusCodes.Status500InternalServerError,
                    detail = "Ocorreu um erro inesperado."
                });
            await context.Response.WriteAsync(corpo, context.RequestAborted);
        }
    }
}
