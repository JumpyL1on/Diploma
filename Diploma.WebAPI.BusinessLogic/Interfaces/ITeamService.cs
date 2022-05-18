using Diploma.Common.DTOs;
using Diploma.Common.Helpers;
using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamService
{
    public Task<Result<object>> CreateAsync(CreateTeamRequest createTeam, Guid userId);
    public Task<Result<TeamDTO>> GetByUserId(Guid userId);
    public Task<Result<object>> DeleteAsync(Guid id, Guid userId);
}