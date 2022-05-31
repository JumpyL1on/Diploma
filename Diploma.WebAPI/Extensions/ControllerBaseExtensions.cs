using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid GetUserId(this ControllerBase controller)
    {
        return Guid.Parse(controller.HttpContext.User.Claims.First(claim => claim.Type == "Id").Value);
    }
}