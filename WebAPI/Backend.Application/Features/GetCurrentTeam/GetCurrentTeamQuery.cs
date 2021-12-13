using Backend.Application.Base;
using Backend.Application.DTOs;
using MediatR;

namespace Backend.Application.Features.GetCurrentTeam;

public class GetCurrentTeamQuery : BaseQuery, IRequest<TeamDTO>
{
}