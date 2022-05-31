using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class OrganizationMemberService : IOrganizationMemberService
{
    private readonly AppDbContext _dbContext;

    public OrganizationMemberService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task CreateAsync(Guid organizationId, Guid userId)
    {
        var organizationMember = new OrganizationMember
        {
            Role = "Судья",
            OrganizationId = organizationId,
            UserId = userId
        };

        _dbContext.OrganizationMembers.Add(organizationMember);

        await _dbContext.SaveChangesAsync();
    }
}