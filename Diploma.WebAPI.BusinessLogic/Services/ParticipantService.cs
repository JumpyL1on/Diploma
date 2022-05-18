using Diploma.Common.Helpers;
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

    public async Task<Result<object>> CreateParticipantAsync(Guid tournamentId, Guid userId)
    {
        var teamId = await _dbContext.TeamMembers
            .Where(teamMember => teamMember.UserId == userId)
            .Select(teamMember => teamMember.TeamId)
            .SingleOrDefaultAsync();

        var participant = new Participant
        {
            TeamId = teamId,
            TournamentId = tournamentId
        };

        _dbContext.Participants.Add(participant);

        await _dbContext.SaveChangesAsync();

        var tournament = await _dbContext.Tournaments
            .SingleAsync(tournament => tournament.Id == tournamentId);
            
        tournament.ParticipantsNumber++;
        
        await _dbContext.SaveChangesAsync();

        return new CreatedResult<object>();
    }
}