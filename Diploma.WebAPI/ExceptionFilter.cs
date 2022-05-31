using Diploma.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Diploma.WebAPI
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case AuthorizationException:
                    HandleException(context, StatusCodes.Status403Forbidden);

                    break;
                case BusinessException:
                case ValidationException:
                    HandleException(context, StatusCodes.Status400BadRequest);

                    break;
                case NotFoundException:
                    HandleException(context, StatusCodes.Status404NotFound);

                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    
                    context.Result = new ContentResult
                    {
                        Content = context.Exception.Message
                    };
                    
                    break;
            }
        }

        private void HandleException(ExceptionContext context, int status)
        {
            context.HttpContext.Response.StatusCode = status;

            context.Result = new ContentResult
            {
                Content = context.Exception.Message
            };
        }
    }
}