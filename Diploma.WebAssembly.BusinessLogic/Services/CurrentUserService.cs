using System.Net.Http.Json;
using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly HttpClient _httpClient;

    public CurrentUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TeamDTO>> GetAllTeamsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TeamDTO>>("users/current/teams") ?? new List<TeamDTO>();
    }

    public async Task<List<GameDTO>> GetAllGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GameDTO>>("users/current/games") ?? new List<GameDTO>();
    }

    public async Task<List<OrganizationDTO>> GetAllOrganizationsAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<OrganizationDTO>>("users/current/organizations") ?? new List<OrganizationDTO>();
    }
}