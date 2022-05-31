using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task CreateAsync(CreateTeamRequest request, Guid userId);
    public Task DeleteAsync(Guid id, Guid userId);
}