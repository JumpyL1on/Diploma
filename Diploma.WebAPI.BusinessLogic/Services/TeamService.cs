using AutoMapper;
using Diploma.Common.Exceptions;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamService : ITeamService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public TeamService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(CreateTeamRequest request, Guid userId)
    {
        var gameId = await _dbContext.Games
            .Where(x => x.Title == request.Game)
            .Select(x => x.Id)
            .SingleOrDefaultAsync();
        
        var team = new Team
        {
            Title = request.Title,
            GameId = gameId
        };

        _dbContext.Teams.Add(team);

        await _dbContext.SaveChangesAsync();

        var teamMember = new TeamMember
        {
            Role = "Капитан",
            UserId = userId,
            TeamId = team.Id
        };

        _dbContext.TeamMembers.Add(teamMember);

        await _dbContext.SaveChangesAsync();
        //await UserManager<>.AddClaimAsync(request.AppUser, new Claim("Role", "Owner"));
        //await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var team = await _dbContext.Teams
            .SingleOrDefaultAsync(team => team.Id == id);

        if (team == null)
        {
            throw new BusinessException("Команды не существует");
        }

        _dbContext.Teams.Remove(team);

        await _dbContext.SaveChangesAsync();
    }
}