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

    public Task<List<MatchDTO>> GetAllByTournamentId(Guid tournamentId)
    {
        var requestUri = $"tournaments/{tournamentId}/matches";

        return _httpClient.GetFromJsonAsync<List<MatchDTO>>(requestUri);
    }

    public Task<MatchDetailsDTO> GetByIdAsync(Guid id)
    {
        var requestUri = $"tournaments/{Guid.Empty}/matches/{id}";

        return _httpClient.GetFromJsonAsync<MatchDetailsDTO>(requestUri);
    }
}