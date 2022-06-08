namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamMemberService
{
    public Task CreateAsync(Guid teamId, Guid userId);
    public Task DeleteAsync(Guid id, Guid userId);
}