using Diploma.Common.Helpers;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamMemberService : ITeamMemberService
{
    public TeamMemberService(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    private AppDbContext AppDbContext { get; }
    
    public async Task<Result<object>> CreateAsync(Guid userId, Guid teamId)
    {
        var teamMember = new TeamMember
        {
            Role = "Игрок",
            UserId = userId,
            TeamId = teamId
        };
        
        await AppDbContext.TeamMembers.AddAsync(teamMember);
        
        await AppDbContext.SaveChangesAsync();
        
        //await UserManager<>.AddClaimAsync(request.AppUser, new Claim("Role", "Participant"));
        return new CreatedResult<object>();
    }

    public async Task<Result<object>> DeleteAsync(Guid id, Guid userId)
    {
        var teamMember = await AppDbContext.TeamMembers
            .SingleOrDefaultAsync(teamMember => teamMember.Id == id);

        if (teamMember == null)
        {
            return new UnprocessableEntityResult<object>("Участника команды не существует");
        }
        
        AppDbContext.TeamMembers.Remove(teamMember);
        
        await AppDbContext.SaveChangesAsync();

        return new NoContentResult<object>();
    }
}