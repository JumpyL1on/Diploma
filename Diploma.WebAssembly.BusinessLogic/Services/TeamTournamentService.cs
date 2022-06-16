using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TeamTournamentService : ITeamTournamentService
{
    private readonly HttpClient _httpClient;

    public TeamTournamentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(Guid tournamentId)
    {
        await _httpClient.PostAsync($"tournaments/{tournamentId}/participants", null);
    }
}