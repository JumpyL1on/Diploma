using System;
using Backend.Application.DTOs;
using MediatR;

namespace Backend.Application.Features.GetTournamentById
{
    public class GetTournamentByIdQuery : IRequest<TournamentDTO>
    {
        public Guid Id { get; set; }
    }
}