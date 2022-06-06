using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Diploma.WebAPI.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid GetUserId(this ControllerBase controller)
    {
        var claims = controller.HttpContext.User.Claims;

        return Guid.Parse(claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value);
    }
}