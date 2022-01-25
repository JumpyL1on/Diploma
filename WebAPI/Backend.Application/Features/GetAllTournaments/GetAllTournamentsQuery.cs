using Backend.Application.DTOs;
using MediatR;

namespace Backend.Application.Features.GetAllTournaments;

public class GetAllTournamentsQuery : IRequest<TournamentPreviewDTO[]>
{
}