using Backend.Application.Base;
using MediatR;

namespace Backend.Application.Features.DeleteTeamMember
{
    public record DeleteTeamMemberCommand : BaseCommand, IRequest<Unit>
    {
    }
}