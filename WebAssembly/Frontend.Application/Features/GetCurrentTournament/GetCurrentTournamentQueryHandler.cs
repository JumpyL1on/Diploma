using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using Frontend.Domain.Entities;
using Frontend.Domain.ValueObjects;
using MediatR;

namespace Frontend.Application.Features.GetCurrentTournament;

internal class GetCurrentTournamentQueryHandler : BaseHandler,
    IRequestHandler<GetCurrentTournamentQuery, CurrentTournamentDTO>
{
    public GetCurrentTournamentQueryHandler(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<CurrentTournamentDTO> Handle(GetCurrentTournamentQuery request,
        CancellationToken cancellationToken)
    {
        return await HttpClient.GetFromJsonAsync<CurrentTournamentDTO>("tournaments/current", cancellationToken);
    }
}