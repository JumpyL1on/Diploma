using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITournamentService
{
    public Task<List<TournamentDTO>> GetAllByStatus(string status);
    public Task<TournamentDetailsDTO> GetById(Guid id);
    public Task CreateAsync(CreateTournamentRequest request);
}