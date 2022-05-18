using Diploma.Common.DTOs;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IMatchService
{
    public Task<MatchDTO?> GetCurrentMatch();
}