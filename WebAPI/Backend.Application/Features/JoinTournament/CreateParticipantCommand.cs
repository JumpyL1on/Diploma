using System;
using Backend.Application.Base;
using MediatR;

namespace Backend.Application.Features.JoinTournament;

public record CreateParticipantCommand : BaseCommand, IRequest<Unit>
{
    public Guid TournamentId { get; set; }
}