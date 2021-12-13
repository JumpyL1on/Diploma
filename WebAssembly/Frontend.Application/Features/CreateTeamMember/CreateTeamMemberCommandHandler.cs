using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.CreateTeamMember
{
    public class CreateTeamMemberCommandHandler : BaseHandler, IRequestHandler<CreateTeamMemberCommand, Unit>
    {
        public CreateTeamMemberCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Unit> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
        {
            await HttpClient.PostAsync($"teams/{request.Id}/team-members", null, cancellationToken);
            return Unit.Value;
        }
    }
}