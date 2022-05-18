﻿using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.Controllers;

[ApiController]
[Route("api/matches")]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrentMatch()
    {
        var result = await _matchService.GetCurrentMatch(this.GetUserId());
        return this.FromResult(result);
    }
}