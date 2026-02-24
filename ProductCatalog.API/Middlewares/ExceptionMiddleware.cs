using System.Net;

using System.Text.Json;



namespace ProductCatalog.API.Middlewares;



public class ExceptionMiddleware

{

    private readonly RequestDelegate _next;



    public ExceptionMiddleware(RequestDelegate next)

    {

        _next = next;

    }



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



    private static Task HandleExceptionAsync(HttpContext context, Exception exception)

    {

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        string message = "Ocorreu um erro interno.";



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