using System;
using MediatR;

namespace Frontend.Application.Features.DeleteTeam
{
    public class DeleteTeamCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}