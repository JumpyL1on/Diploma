using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/users/current")]
[Authorize]
public class CurrentUserController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public CurrentUserController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [Route("teams")]
    public async Task<IActionResult> GetAllTeamsAsync()
    {
        return Ok(await _currentUserService.GetAllTeamsAsync(this.GetUserId()));
    }

    [HttpGet]
    [Route("games")]
    public async Task<IActionResult> GetAllGamesAsync()
    {
        return Ok(await _currentUserService.GetAllGamesAsync(this.GetUserId()));
    }

    [HttpGet]
    [Route("organizations")]
    public async Task<IActionResult> GetAllOrganizationsAsync()
    {
        return Ok(await _currentUserService.GetAllOrganizationsAsync(this.GetUserId()));
    }
}