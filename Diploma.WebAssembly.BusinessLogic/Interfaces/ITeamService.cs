using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task<TeamDetailsDTO> GetByIdAsync(Guid id);
    public Task CreateAsync(CreateTeamRequest request);
    public Task DeleteAsync(Guid id);
}