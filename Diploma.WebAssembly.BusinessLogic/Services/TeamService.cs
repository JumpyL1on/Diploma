using System.Net.Http.Json;
using Blazored.LocalStorage;
using Diploma.Common.DTOs;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TeamService : ITeamService
{
    public TeamService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        HttpClient = httpClient;
        LocalStorageService = localStorageService;
    }

    private HttpClient HttpClient { get; }
    private ILocalStorageService LocalStorageService { get; }

    public async Task CreateAsync(CreateTeamRequest request)
    {
        await HttpClient.PostAsJsonAsync("teams", request);
    }

    public async Task<TeamDTO?> GetCurrentAsync()
    {
        return await HttpClient.GetFromJsonAsync<TeamDTO>("teams/current");
    }

    public async Task DeleteAsync(Guid id)
    {
        await HttpClient.DeleteAsync($"teams/{id}");
    }
}