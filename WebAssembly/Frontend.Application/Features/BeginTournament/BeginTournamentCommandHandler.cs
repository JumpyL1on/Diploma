using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.BeginTournament;

internal class BeginTournamentCommandHandler : BaseHandler, IRequestHandler<BeginTournamentCommand, Unit>
{
    public BeginTournamentCommandHandler(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Unit> Handle(BeginTournamentCommand request, CancellationToken cancellationToken)
    {
        await HttpClient.PutAsJsonAsync($"tournaments/{request.TournamentId}", request, cancellationToken);
        return Unit.Value;
    }
}