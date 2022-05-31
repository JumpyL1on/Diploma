using System.Net.Http.Json;
using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TournamentService : ITournamentService
{
    private readonly HttpClient _httpClient;

    public TournamentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TournamentDTO>> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<List<TournamentDTO>>("tournaments?status=current");
    }

    public async Task<TournamentDetailsDTO> GetById(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<TournamentDetailsDTO>($"tournaments/{id}");
    }
}