using System.Net.Http.Json;
using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class MatchService : IMatchService
{
    private readonly HttpClient _httpClient;

    public MatchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MatchDTO?> GetCurrentMatch()
    {
        return await _httpClient.GetFromJsonAsync<MatchDTO>("matches/current");
    }
}