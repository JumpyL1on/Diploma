using Diploma.Common.Helpers;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IParticipantService
{
    public Task<Result<object>> CreateParticipantAsync(Guid tournamentId, Guid userId);
}