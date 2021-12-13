using System.Net.Http;
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

    public Task<Unit> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}