namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IParticipantService
{
    public Task CreateParticipantAsync(Guid tournamentId, Guid userId);
}