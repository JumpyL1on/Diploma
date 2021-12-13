using System;
using Backend.Application.Base;
using MediatR;

namespace Backend.Application.Features.CreateTeam
{
    public record CreateTeamCommand : BaseCommand, IRequest<Guid>
    {
        public string Title { get; set; }
    }
}