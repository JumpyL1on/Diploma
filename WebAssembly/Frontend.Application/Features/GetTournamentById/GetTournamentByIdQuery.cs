using System;
using Frontend.Domain.ValueObjects;
using MediatR;

namespace Frontend.Application.Features.GetTournamentById
{
    public class GetTournamentByIdQuery : IRequest<TournamentDTO>
    {
        public Guid Id { get; set; }
    }
}