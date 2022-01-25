using System;
using MediatR;

namespace Frontend.Application.Features.JoinTournament
{
    public class JoinTournamentCommand : IRequest
    {
        public Guid TournamentId { get; set; }
    }
}