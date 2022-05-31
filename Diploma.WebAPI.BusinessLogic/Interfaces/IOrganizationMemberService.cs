namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IOrganizationMemberService
{
    public Task CreateAsync(Guid organizationId, Guid userId);
}