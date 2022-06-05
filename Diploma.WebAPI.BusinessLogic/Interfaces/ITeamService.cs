using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task<TeamDetailsDTO> GetByIdAsync(Guid id);
    public Task CreateAsync(CreateTeamRequest request, Guid userId);
    public Task DeleteAsync(Guid id, Guid userId);
}