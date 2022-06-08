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

    public async Task<List<TournamentDTO>> GetAllTournamentsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TournamentDTO>>("users/current/tournaments");
    }

    public async Task<List<TeamDTO>> GetAllTeamsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TeamDTO>>("users/current/teams") ?? new List<TeamDTO>();
    }

    public async Task<List<UserGameDTO>> GetAllGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserGameDTO>>("users/current/games") ?? new List<UserGameDTO>();
    }

    public async Task<List<MatchDTO>> GetAllMatchesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<MatchDTO>>("users/current/matches");
    }

    public async Task<List<OrganizationDTO>> GetAllOrganizationsAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<OrganizationDTO>>("users/current/organizations") ?? new List<OrganizationDTO>();
    }

    public async Task InviteToLobbyAsync()
    {
        await _httpClient.PostAsync("users/current/invite-to-lobby", null);
    }
}