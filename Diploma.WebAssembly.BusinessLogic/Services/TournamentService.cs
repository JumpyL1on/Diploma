using System.Net.Http.Json;
using Diploma.Common.DTOs;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TournamentService : ITournamentService
{
    private readonly HttpClient _httpClient;

    public TournamentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TournamentDTO>> GetAllByStatus(string status)
    {
        var requestUri = $"tournaments/?status={status}";
        
        var tournaments = await _httpClient.GetFromJsonAsync<List<TournamentDTO>>(requestUri);

        return tournaments ?? new List<TournamentDTO>();
    }

    public async Task<TournamentDetailsDTO> GetById(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<TournamentDetailsDTO>($"tournaments/{id}");
    }

    public async Task CreateAsync(CreateTournamentRequest request)
    {
        await _httpClient.PostAsJsonAsync("tournaments", request);
    }
}