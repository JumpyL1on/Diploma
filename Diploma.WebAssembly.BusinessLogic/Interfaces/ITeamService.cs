using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task CreateAsync(CreateTeamRequest request);
    public Task<TeamDTO?> GetCurrentAsync();
    public Task DeleteAsync(Guid id);
}