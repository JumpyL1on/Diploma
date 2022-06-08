using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TeamTournamentService : ITeamTournamentService
{
    private readonly AppDbContext _dbContext;

    public TeamTournamentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Guid tournamentId, Guid userId)
    {
        var teamId = await _dbContext.TeamMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.TeamId)
            .SingleOrDefaultAsync();

        var teamTournament = new TeamTournament
        {
            TeamId = teamId,
            TournamentId = tournamentId
        };

        _dbContext.TeamTournaments.Add(teamTournament);

        var tournament = await _dbContext.Tournaments
            .SingleAsync(tournament => tournament.Id == tournamentId);
            
        tournament.ParticipantsNumber++;
        
        await _dbContext.SaveChangesAsync();
    }
}