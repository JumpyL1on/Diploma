using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.BeginTournament;

internal class BeginTournamentCommandHandler : BaseCommandHandler, IRequestHandler<BeginTournamentCommand, Unit>
{
    private Random Random { get; set; } = new();

    public BeginTournamentCommandHandler(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Unit> Handle(BeginTournamentCommand request, CancellationToken cancellationToken)
    {
        var participantIds = await DbContext
            .Set<Participant>()
            .Where(participant => participant.TournamentId == request.TournamentId)
            .Select(participant => participant.Id)
            .ToListAsync(cancellationToken);
        var number = 1;
        for (var i = 0; i < request.MaxParticipantsNumber / 2; i++)
        {
            var index = Random.Next(participantIds.Count);
            var participantIdA = participantIds[index];
            participantIds.Remove(participantIdA);
            index = Random.Next(participantIds.Count);
            var participantIdB = participantIds[index];
            participantIds.Remove(participantIdB);
            var match = new Match(DateTime.Now, participantIdA, participantIdB, request.TournamentId, 1, number++);
            DbContext.Entry(match).State = EntityState.Added;
        }

        request.MaxParticipantsNumber /= 2;
        for (var i = 2; request.MaxParticipantsNumber != 1; request.MaxParticipantsNumber /= 2)
        {
            var count = 1;
            for (var j = 0; j < request.MaxParticipantsNumber / 2; j++)
            {
                var match = new Match(DateTime.Now, null, null, request.TournamentId, i, count++);
                DbContext.Entry(match).State = EntityState.Added;
            }

            i++;
        }

        await DbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}