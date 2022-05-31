using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CurrentUserService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<TeamDTO>> GetAllTeamsAsync(Guid userId)
    {
        return await _dbContext.TeamMembers
            .Where(x => x.UserId == userId)
            .Select(x => new TeamDTO
            {
                GameTitle = x.Team.Game.Title,
                Id = x.TeamId,
                Role = x.Role,
                Title = x.Team.Title
            })
            .ToListAsync();
    }

    public async Task<List<GameDTO>> GetAllGamesAsync(Guid userId)
    {
        return await _dbContext.UserGames
            .Where(x => x.UserId == userId)
            .ProjectTo<GameDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<OrganizationDTO>> GetAllOrganizationsAsync(Guid userId)
    {
        return await _dbContext.OrganizationMembers
            .Where(x => x.UserId == userId)
            .Select(x => new OrganizationDTO
            {
                Role = x.Role,
                Title = x.Organization.Title
            })
            .ToListAsync();
    }
}