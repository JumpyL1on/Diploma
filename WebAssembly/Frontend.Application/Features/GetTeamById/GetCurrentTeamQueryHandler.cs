using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using Frontend.Domain.Entities;
using Frontend.Domain.ValueObjects;
using MediatR;

namespace Frontend.Application.Features.GetTeamById
{
    public class GetCurrentTeamQueryHandler : BaseHandler, IRequestHandler<GetCurrentTeamQuery, TeamDTO>
    {
        public GetCurrentTeamQueryHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<TeamDTO> Handle(GetCurrentTeamQuery request, CancellationToken cancellationToken)
        {
            return await HttpClient.GetFromJsonAsync<TeamDTO>($"teams/current", cancellationToken);
        }
    }
}