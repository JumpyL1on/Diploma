using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.JoinTournament;

internal class CreateParticipantCommandCommandHandler : BaseCommandHandler, IRequestHandler<CreateParticipantCommand, Unit>
{
    public CreateParticipantCommandCommandHandler(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Unit> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
    {
        var teamMember = await DbContext
            .Set<TeamMember>()
            .SingleOrDefaultAsync(member => member.AppUserId == request.AppUser.Id, cancellationToken);
        var participant = new Participant(teamMember.TeamId, request.TournamentId);
        DbContext.Entry(participant).State = EntityState.Added;
        await DbContext.SaveChangesAsync(cancellationToken);
        var tournament = await DbContext
            .Set<Tournament>()
            .FindAsync(new object[] { request.TournamentId }, cancellationToken);
        tournament?.JoinParticipant();
        await DbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}