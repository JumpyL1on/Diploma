using Diploma.Common.DTOs;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ICurrentUserService
{
    public Task<List<TeamDTO>> GetAllTeamsAsync();
    public Task<List<GameDTO>> GetAllGamesAsync();
    public Task<List<OrganizationDTO>> GetAllOrganizationsAsync();
}