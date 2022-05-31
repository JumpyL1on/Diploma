using Diploma.Common.Exceptions;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Steam;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly SteamGameClient _gameClient;
    
    public TeamMemberService(AppDbContext appDbContext/*, SteamGameClient gameClient*/)
    {
        AppDbContext = appDbContext;
        //_gameClient = gameClient;
    }

    private AppDbContext AppDbContext { get; }

    public async Task CreateAsync(Guid userId, Guid teamId)
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
    }

    public async Task InviteToLobby(Guid userId)
    {
        var steamId = await AppDbContext.UserGames
            .Where(x => x.UserId == userId)
            .Select(x => x.SteamId)
            .SingleOrDefaultAsync();

        _gameClient.InviteToLobby(steamId);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var teamMember = await AppDbContext.TeamMembers
            .SingleOrDefaultAsync(teamMember => teamMember.Id == id);

        if (teamMember == null)
        {
            throw new BusinessException("Участника команды не существует");
        }

        AppDbContext.TeamMembers.Remove(teamMember);

        await AppDbContext.SaveChangesAsync();
    }
}