namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamTournamentService
{
    public Task CreateAsync(Guid tournamentId, Guid userId);
}