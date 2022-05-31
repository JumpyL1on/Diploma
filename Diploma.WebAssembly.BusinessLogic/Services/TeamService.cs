﻿using System.Net.Http.Json;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TeamService : ITeamService
{
    private readonly HttpClient _httpClient;

    public TeamService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(CreateTeamRequest request)
    {
        await _httpClient.PostAsJsonAsync("teams", request);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"teams/{id}");
    }
}