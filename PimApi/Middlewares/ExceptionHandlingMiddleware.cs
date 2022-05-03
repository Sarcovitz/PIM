using PimApi.Exceptions;
using System.Net;

namespace PimApi.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try { await next(context); }
        catch(ArgumentException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(KeyNotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(KeyAlreadyExistsException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
