using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.JoinTournament;

internal class CreateParticipantCommandHandler : BaseHandler, IRequestHandler<CreateParticipantCommand, Unit>
{
    public CreateParticipantCommandHandler(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Unit> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
    {
        var teamMember = await DbContext
            .Set<TeamMember>()
            .SingleOrDefaultAsync(member => member.AppUserId == request.AppUser.Id, cancellationToken);
        var participant = new Participant(teamMember.Id, request.TournamentId);
        DbContext.Entry(participant).State = EntityState.Added;
        await DbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}