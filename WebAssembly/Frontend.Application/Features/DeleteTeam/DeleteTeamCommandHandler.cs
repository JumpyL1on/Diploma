using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.DeleteTeam
{
    public class DeleteTeamCommandHandler : BaseHandler, IRequestHandler<DeleteTeamCommand, Unit>
    {
        public DeleteTeamCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            await HttpClient.DeleteAsync($"teams/{request.Id}", cancellationToken);
            return Unit.Value;
        }
    }
}