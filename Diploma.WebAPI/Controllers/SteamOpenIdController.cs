using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

public class SteamOpenIdController : ControllerBase
{
    [HttpGet]
    [Route("steam-openid")]
    public new IActionResult Challenge()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("ChallengeCallback")
            },
            SteamAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("steam-openid-callback")]
    [Authorize(AuthenticationSchemes = SteamAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ChallengeCallback()
    {
        var values = HttpContext.User.Claims
            .Select(claim => claim.Value)
            .ToList();

        await HttpContext.SignOutAsync();
        
        return Redirect($"https://localhost:7073/profile/connect?claimedId={values[0]}&nickname={values[1]}");
    }
}