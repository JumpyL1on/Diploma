using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[Route("steam")]
public class SteamController : ControllerBase
{
    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallback")
            },
            SteamAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("login-callback")]
    [Authorize(AuthenticationSchemes = SteamAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> LoginCallback()
    {
        var values = HttpContext.User.Claims
            .Select(claim => claim.Value)
            .ToList();

        await HttpContext.SignOutAsync();
        
        return Redirect($"https://localhost:7073/user/profile/connect?claimedId={values[0]}&nickname={values[1]}");
    }
}