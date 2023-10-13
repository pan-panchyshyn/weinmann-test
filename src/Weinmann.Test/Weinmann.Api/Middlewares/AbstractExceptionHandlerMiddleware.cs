using System.Net;

namespace Weinmann.Api.Middlewares;

public abstract class AbstractExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public AbstractExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public abstract (HttpStatusCode code, string message) GetResponse(Exception exception);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            // log the error
            var response = context.Response;
            response.ContentType = "application/json";

            // get the response code and message
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
    }
}
