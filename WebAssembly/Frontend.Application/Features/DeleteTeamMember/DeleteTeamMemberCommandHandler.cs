using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.DeleteTeamMember
{
    public class DeleteTeamMemberCommandHandler : BaseHandler, IRequestHandler<DeleteTeamMemberCommand, Unit>
    {
        public DeleteTeamMemberCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Unit> Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
        {
            await HttpClient.DeleteAsync($"teams/{request.Id}/team-members", cancellationToken);
            return Unit.Value;
        }
    }
}