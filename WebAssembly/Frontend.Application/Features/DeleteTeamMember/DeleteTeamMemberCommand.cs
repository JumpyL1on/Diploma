using System;
using MediatR;

namespace Frontend.Application.Features.DeleteTeamMember
{
    public class DeleteTeamMemberCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}