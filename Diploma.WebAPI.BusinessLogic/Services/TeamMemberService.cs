using Diploma.Common.Exceptions;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly AppDbContext _appDbContext;

    public TeamMemberService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(Guid teamId, Guid userId)
    {
        var teamMember = new TeamMember
        {
            Role = "Игрок",
            TeamId = teamId,
            UserId = userId
        };

        await _appDbContext.TeamMembers.AddAsync(teamMember);

        await _appDbContext.SaveChangesAsync();

        //await UserManager<>.AddClaimAsync(request.AppUser, new Claim("Role", "Participant"));
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var teamMember = await _appDbContext.TeamMembers
            .SingleOrDefaultAsync(teamMember => teamMember.Id == id);

        if (teamMember == null)
        {
            throw new BusinessException("Участника команды не существует");
        }

        _appDbContext.TeamMembers.Remove(teamMember);

        await _appDbContext.SaveChangesAsync();
    }
}