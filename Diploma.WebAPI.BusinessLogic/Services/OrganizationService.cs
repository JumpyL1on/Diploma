using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class OrganizationService : IOrganizationService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrganizationService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<OrganizationDetailsDTO> GetByIdAsync(Guid id)
    {
        return _dbContext.Organizations
            .Where(x => x.Id == id)
            .ProjectTo<OrganizationDetailsDTO>(_mapper.ConfigurationProvider)
            .SingleAsync();
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