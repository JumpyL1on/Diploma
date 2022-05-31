using Diploma.Common.DTOs;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IMatchService
{
    public Task<MatchDTO?> GetCurrentMatch(Guid userId);
    //public void Begin(Guid id);
}