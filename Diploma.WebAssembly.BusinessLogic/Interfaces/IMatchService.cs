using Diploma.Common.DTOs;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IMatchService
{
    public Task<List<MatchDTO>> GetAllByTournamentId(Guid tournamentId);
    public Task<MatchDetailsDTO> GetByIdAsync(Guid id);
}