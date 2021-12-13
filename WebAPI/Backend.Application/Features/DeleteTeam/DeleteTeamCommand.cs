using System;
using Backend.Application.Base;
using MediatR;

namespace Backend.Application.Features.DeleteTeam
{
    public record DeleteTeamCommand : BaseCommand, IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}