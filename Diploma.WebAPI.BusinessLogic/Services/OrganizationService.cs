using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class OrganizationService : IOrganizationService
{
    private readonly AppDbContext _dbContext;

    public OrganizationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task CreateAsync(CreateOrganizationRequest request, Guid userId)
    {
        var organization = new Organization
        {
            Title = request.Title
        };

        _dbContext.Organizations.Add(organization);

        await _dbContext.SaveChangesAsync();

        var teamMember = new OrganizationMember
        {
            Role = "Владелец",
            UserId = userId,
            OrganizationId = organization.Id
        };

        _dbContext.OrganizationMembers.Add(teamMember);

        await _dbContext.SaveChangesAsync();
    }
}