using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.CreateTournament;

internal class CreateTournamentCommandCommandHandler : BaseCommandHandler, IRequestHandler<CreateTournamentCommand, Unit>
{
    public CreateTournamentCommandCommandHandler(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Unit> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        var tournament = new Tournament(request.Title, request.RegistrationStart, request.RegistrationEnd,
            request.Start, request.End, request.ParticipantsNumber);
        DbContext.Entry(tournament).State = EntityState.Added;
        await DbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}