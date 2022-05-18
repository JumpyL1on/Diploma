using Diploma.Common.Helpers;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamMemberService
{
    public Task<Result<object>> CreateAsync(Guid userId, Guid teamId);
    public Task<Result<object>> DeleteAsync(Guid id, Guid userId);
}