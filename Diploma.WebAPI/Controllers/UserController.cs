using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
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
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpUserRequest signUpUser)
    {
        var result = await _userService.SignUpAsync(signUpUser);
        return this.FromResult(result);
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInUserRequest signInUser)
    {
        var result = await _userService.SignInAsync(signInUser);
        return this.FromResult(result);
    }
}