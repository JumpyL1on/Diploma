using Diploma.Common.DTOs;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IMatchService
{
    public Task<List<MatchDTO>> GetAllByTournamentId(Guid id);
    public Task<MatchDetailsDTO> GetById(Guid id, Guid userId);
    public Task CreateAsync(Guid id);
    public Task StartAsync(Guid id);
}