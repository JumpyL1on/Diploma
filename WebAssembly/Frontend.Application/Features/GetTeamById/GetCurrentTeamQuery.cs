using System;
using Frontend.Domain.Entities;
using Frontend.Domain.ValueObjects;
using MediatR;

namespace Frontend.Application.Features.GetTeamById
{
    public class GetCurrentTeamQuery : IRequest<TeamDTO>
    {
    }
}