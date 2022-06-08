using Diploma.Common.DTOs;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ICurrentUserService
{
    public Task<List<TournamentDTO>> GetAllTournamentsAsync(Guid userId);
    public Task<List<TeamDTO>> GetAllTeamsAsync(Guid userId);
    public Task<List<UserGameDTO>> GetAllGamesAsync(Guid userId);
    public Task<List<MatchDTO>> GetAllMatchesAsync(Guid userId);
    public Task<List<OrganizationDTO>> GetAllOrganizationsAsync(Guid userId);
    public Task InviteToLobbyAsync(Guid userId);
}