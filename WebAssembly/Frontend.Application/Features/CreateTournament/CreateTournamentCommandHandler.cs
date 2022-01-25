using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.CreateTournament;

internal class CreateTournamentCommandHandler : BaseHandler, IRequestHandler<CreateTournamentCommand>
{
    public CreateTournamentCommandHandler(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Unit> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        await HttpClient.PostAsJsonAsync("tournaments", request, cancellationToken);
        return Unit.Value;
    }
}