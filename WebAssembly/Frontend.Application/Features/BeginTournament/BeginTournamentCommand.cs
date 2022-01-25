using System;
using MediatR;

namespace Frontend.Application.Features.BeginTournament;

public class BeginTournamentCommand : IRequest
{
    public Guid TournamentId { get; set; }
    public int MaxParticipantsNumber { get; set; }
}