using Diploma.Common.DTOs;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ICurrentUserService
{
    public Task<List<TeamDTO>> GetAllTeamsAsync(Guid userId);
    public Task<List<GameDTO>> GetAllGamesAsync(Guid userId);
    public Task<List<OrganizationDTO>> GetAllOrganizationsAsync(Guid userId);
}