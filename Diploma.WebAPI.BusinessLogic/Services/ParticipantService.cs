using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class ParticipantService : IParticipantService
{
    private readonly AppDbContext _dbContext;

    public ParticipantService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateParticipantAsync(Guid tournamentId, Guid userId)
    {
        var teamId = await _dbContext.TeamMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.TeamId)
            .SingleOrDefaultAsync();

        var participant = new Participant
        {
            TeamId = teamId,
            TournamentId = tournamentId
        };

        _dbContext.Participants.Add(participant);

        var tournament = await _dbContext.Tournaments
            .SingleAsync(tournament => tournament.Id == tournamentId);
            
        tournament.ParticipantsNumber++;
        
        await _dbContext.SaveChangesAsync();
    }
}