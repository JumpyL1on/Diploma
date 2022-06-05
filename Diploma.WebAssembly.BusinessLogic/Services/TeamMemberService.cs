using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly HttpClient _httpClient;

    public TeamMemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(Guid teamId)
    {
        await _httpClient.PostAsync($"teams/{teamId}/team-members", null);
    }
}