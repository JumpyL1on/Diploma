using Diploma.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Diploma.WebAPI;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BusinessException:
            case ValidationException:
                HandleException(context, StatusCodes.Status400BadRequest);

                break;
            case AuthorizationException:
                HandleException(context, StatusCodes.Status403Forbidden);

                break;
            case NotFoundException:
                HandleException(context, StatusCodes.Status404NotFound);

                break;
            default:
                HandleException(context, StatusCodes.Status500InternalServerError);

                break;
        }
    }

    private static void HandleException(ExceptionContext context, int status)
    {
        context.Result = new ContentResult
        {
            Content = context.Exception.Message
        };

        context.HttpContext.Response.StatusCode = status;
    }
}