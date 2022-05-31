using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/user-games")]
[Authorize]
public class UserGameController : ControllerBase
{
    private readonly IUserGameService _userGameService;

    public UserGameController(IUserGameService userGameService)
    {
        _userGameService = userGameService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateUserGameRequest request)
    {
        await _userGameService.CreateAsync(request, this.GetUserId());

        return Ok();
    }
}