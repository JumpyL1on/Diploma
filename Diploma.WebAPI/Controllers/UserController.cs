using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpUserRequest request)
    {
        await _userService.SignUpUserAsync(request);

        return Ok();
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInUserRequest request)
    {
        return Ok(await _userService.SignInUserAsync(request));
    }
}