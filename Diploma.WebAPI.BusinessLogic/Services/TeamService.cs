using Diploma.Common.DTOs;
using Diploma.Common.Helpers;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamService : ITeamService
{
    public TeamService(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    private AppDbContext AppDbContext { get; }

    public async Task<Result<object>> CreateAsync(CreateTeamRequest createTeam, Guid userId)
    {
        var team = new Team
        {
            Title = createTeam.Title,
            Tag = createTeam.Tag
        };

        AppDbContext.Teams.Add(team);

        await AppDbContext.SaveChangesAsync();

        var teamMember = new TeamMember
        {
            Role = "Капитан",
            UserId = userId,
            TeamId = team.Id
        };

        AppDbContext.TeamMembers.Add(teamMember);

        await AppDbContext.SaveChangesAsync();

        //await UserManager<>.AddClaimAsync(request.AppUser, new Claim("Role", "Owner"));
        //await DbContext.SaveChangesAsync(cancellationToken);
        return new CreatedResult<object>();
    }

    public async Task<Result<TeamDTO>> GetByUserId(Guid userId)
    {
        var team = await AppDbContext.Teams
            .Where(team => team.TeamMembers.Any(teamMember => teamMember.UserId == userId))
            .Select(team => new TeamDTO
            {
                Id = team.Id,
                Title = team.Title,
                TeamMembers = team.TeamMembers
                    .Select(teamMember => new TeamMemberDTO
                    {
                        FullName = $@"{teamMember.User.Name} {teamMember.User.UserName} {teamMember.User.Surname}",
                        Role = teamMember.Role
                    })
                    .ToList()
            })
            .SingleOrDefaultAsync();

        if (team == null)
        {
            return new NotFoundResult<TeamDTO>("Пользователь не состоит в этой команде");
        }

        return new OkResult<TeamDTO>(team);
    }

    public async Task<Result<object>> DeleteAsync(Guid id, Guid userId)
    {
        var team = await AppDbContext.Teams
            .SingleOrDefaultAsync(team => team.Id == id);

        if (team == null)
        {
            return new UnprocessableEntityResult<object>("Команды не существует");
        }

        AppDbContext.Teams.Remove(team);

        await AppDbContext.SaveChangesAsync();
        return new NoContentResult<object>();
    }
}