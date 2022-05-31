namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamMemberService
{
    public Task CreateAsync(Guid userId, Guid teamId);
    public Task InviteToLobby(Guid userId);
    public Task DeleteAsync(Guid id, Guid userId);
}