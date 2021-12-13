using Frontend.Domain.Entities;
using MediatR;

namespace Frontend.Application.Features.GetAllTournaments
{
    public class GetAllTournamentsQuery : IRequest<TournamentPreviewDTO[]>
    {
    }
}