using System;
using MediatR;

namespace Frontend.Application.Features.JoinTournament
{
    public class JoinTournamentCommand : IRequest<Unit>
    {
        public Guid TournamentId { get; set; }
    }
}