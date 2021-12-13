using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using Frontend.Domain.Entities;
using MediatR;

namespace Frontend.Application.Features.GetAllTournaments
{
    public class GetAllTournamentsQueryHandler : BaseHandler, IRequestHandler<GetAllTournamentsQuery, TournamentPreviewDTO[]>
    {
        public GetAllTournamentsQueryHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<TournamentPreviewDTO[]> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            return await HttpClient.GetFromJsonAsync<TournamentPreviewDTO[]>("tournaments", cancellationToken);
        }
    }
}