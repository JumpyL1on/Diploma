namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface ITeamMemberService
{
    public Task CreateAsync(Guid teamId);
}