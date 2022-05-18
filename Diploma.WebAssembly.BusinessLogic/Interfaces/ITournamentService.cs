using Diploma.Common.DTOs;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITournamentService
{
    public Task<List<TournamentDTO>> GetAll();
    public Task<TournamentDetailsDTO> GetById(Guid id);
}