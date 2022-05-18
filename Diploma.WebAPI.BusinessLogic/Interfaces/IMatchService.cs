using Diploma.Common.DTOs;
using Diploma.Common.Helpers;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IMatchService
{
    public Task<Result<MatchDTO>> GetCurrentMatch(Guid userId);
}