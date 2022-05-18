using System.ComponentModel;
using Diploma.Common.Enums;
using Diploma.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult FromResult<T>(this ControllerBase controller, Result<T> result)
    {
        return result.ResultType switch
        {
            ResultType.Ok => controller.Ok(result.Data),
            ResultType.Created => new ObjectResult(null) { StatusCode = StatusCodes.Status201Created },
            ResultType.NoContent => controller.NoContent(),
            ResultType.Forbidden => new ObjectResult(result.Errors) { StatusCode = StatusCodes.Status403Forbidden },
            ResultType.NotFound => controller.NotFound(result.Errors),
            ResultType.UnprocessableEntity => controller.UnprocessableEntity(result.Errors),
            _ => throw new InvalidEnumArgumentException()
        };
    }

    public static Guid GetUserId(this ControllerBase controller)
    {
        return Guid.Parse(controller.HttpContext.User.Claims.First(claim => claim.Type == "Id").Value);
    }
}