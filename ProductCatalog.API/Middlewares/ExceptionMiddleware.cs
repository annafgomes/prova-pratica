using System.Net;
using System.Text.Json;

namespace ProductCatalog.API.Middlewares;

/// <summary>
/// Middleware global para captura e tratamento de excecoes da aplicacao
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Construtor que recebe o proximo delegado no pipeline de execucao
    /// </summary>
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Tenta executar a requisicao e captura eventuais erros nao tratados
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Formata a resposta de erro baseada no tipo da excecao capturada
    /// </summary>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Ocorreu um erro interno.";

        /// <summary>
        /// Mapeia tipos especificos de excecao para codigos HTTP adequados
        /// </summary>
        if (exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = exception.Message;
        }
        else if (exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = exception.Message;
        }

        var response = new
        {
            status = (int)statusCode,
            error = message
        };

        var json = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(json);
    }
}