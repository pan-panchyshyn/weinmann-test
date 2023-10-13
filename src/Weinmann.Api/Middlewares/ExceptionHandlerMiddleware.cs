using System.Net;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Weinmann.BusinessLogic;

namespace Weinmann.Api.Middlewares;

public class ExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    public ExceptionHandlerMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        string message;
        switch (exception)
        {
            case KeyNotFoundException
                or FileNotFoundException
                or EntityNotFoundException:
                code = HttpStatusCode.NotFound;
                message = exception.Message ?? "Entity not found";
                break;
            case UnauthorizedAccessException
             or ArgumentException:
                code = HttpStatusCode.Unauthorized;
                message = exception.Message ?? "UserId or password is not valid";
                break;
            case InvalidOperationException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message ?? "Invalid operation";
                break;
            case SqlException:
                code = HttpStatusCode.InternalServerError;
                message = "The database is not found or not accessible";
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                message = exception.Message ?? "Something went wrong";
                break;
        }

        return (code, JsonConvert.SerializeObject(new ErrorResponse { ErrorMessage = message }));
    }
}