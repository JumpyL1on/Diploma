namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IParticipantService
{
    public Task CreateAsync(Guid tournamentId);
}