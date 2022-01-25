using Frontend.Domain.Entities;
using MediatR;

namespace Frontend.Application.Features.GetTeamById
{
    public class GetCurrentTeamQuery : IRequest<TeamDTO>
    {
    }
}