using Backend.Application.Base;
using Backend.Application.DTOs;
using MediatR;

namespace Backend.Application.Features.GetCurrentTournament;

public class GetCurrentTournamentQuery : BaseQuery, IRequest<CurrentTournamentDTO>
{
    
}