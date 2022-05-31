using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task CreateAsync(CreateTeamRequest request);
    public Task DeleteAsync(Guid id);
}