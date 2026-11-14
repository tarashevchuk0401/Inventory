using InventoryHub.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryHub.Api.Middleware;

public class AppExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is DbUpdateException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "ConflictException",
                    Detail = "This operation conflicts with existing related data."
                }
            });
        }

        if (exception is not AppException appException)
        {
            return false;
        }

        httpContext.Response.StatusCode = appException.StatusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = appException.StatusCode,
                Title = appException.GetType().Name,
                Detail = appException.Message
            }
        });
    }
}
