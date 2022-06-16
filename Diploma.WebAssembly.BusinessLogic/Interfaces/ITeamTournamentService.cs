namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITeamTournamentService
{
    public Task CreateAsync(Guid tournamentId);
}