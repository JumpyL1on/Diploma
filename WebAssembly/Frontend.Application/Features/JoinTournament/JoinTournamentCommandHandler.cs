using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.JoinTournament
{
    internal class JoinTournamentCommandHandler : BaseHandler, IRequestHandler<JoinTournamentCommand, Unit>
    {
        public JoinTournamentCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Unit> Handle(JoinTournamentCommand request, CancellationToken cancellationToken)
        {
            await HttpClient.PostAsync($"tournaments/{request.TournamentId}/participants", null!, cancellationToken);
            return Unit.Value;
        }
    }
}