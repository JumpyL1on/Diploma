using System;
using MediatR;

namespace Frontend.Application.Features.CreateTeamMember
{
    public class CreateTeamMemberCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}