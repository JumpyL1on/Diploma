using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITournamentService
{
    public Task<List<TournamentDTO>> GetUpcomingTournaments();
    public Task<List<TournamentDTO>> GetCurrentTournaments();
    public Task<List<TournamentDTO>> GetFinishedTournaments();
    public Task<TournamentDetailsDTO> GetById(Guid id, Guid userId);
    public Task CreateTournamentAsync(CreateTournamentRequest request, Guid userId);
    public Task BeginTournament(Guid id, DateTime start, int participantNumber);
}