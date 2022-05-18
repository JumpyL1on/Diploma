using Diploma.Common.DTOs;
using Diploma.Common.Helpers;
using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITournamentService
{
    public Task<Result<List<TournamentDTO>>> GetAll();
    public Task<Result<TournamentDetailsDTO>> GetById(Guid id);
    public Task<Result<object>> CreateTournamentAsync(CreateTournamentRequest request);
    public Task BeginTournament(Guid id, DateTime start);
}