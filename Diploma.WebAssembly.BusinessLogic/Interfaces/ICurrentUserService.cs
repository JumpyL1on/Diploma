using Diploma.Common.DTOs;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ICurrentUserService
{
    public Task<List<TournamentDTO>> GetAllTournamentsAsync();
    public Task<List<TeamDTO>> GetAllTeamsAsync();
    public Task<List<UserGameDTO>> GetAllGamesAsync();
    public Task<List<MatchDTO>> GetAllMatchesAsync();
    public Task<List<OrganizationDTO>> GetAllOrganizationsAsync();
    public Task InviteToLobbyAsync(Guid matchId);
}