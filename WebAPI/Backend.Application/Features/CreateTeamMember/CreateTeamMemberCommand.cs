using System;
using Backend.Application.Base;
using MediatR;

namespace Backend.Application.Features.CreateTeamMember
{
    public record CreateTeamMemberCommand : BaseCommand, IRequest<CreateTeamMemberResponse>
    {
        public Guid TeamId { get; set; }
    }
}