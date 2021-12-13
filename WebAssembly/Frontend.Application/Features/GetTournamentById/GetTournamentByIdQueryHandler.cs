using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using Frontend.Domain.ValueObjects;
using MediatR;

namespace Frontend.Application.Features.GetTournamentById
{
    internal class GetTournamentByIdQueryHandler : BaseHandler, IRequestHandler<GetTournamentByIdQuery, TournamentDTO>
    {
        public GetTournamentByIdQueryHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<TournamentDTO> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            return await HttpClient.GetFromJsonAsync<TournamentDTO>($"tournaments/{request.Id}", cancellationToken);
        }
    }
}