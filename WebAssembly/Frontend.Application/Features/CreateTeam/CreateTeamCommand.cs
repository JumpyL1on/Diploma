using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.CreateTeam
{
    public class CreateTeamCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "Название обязательно для заполнения")]
        public string Title { get; set; }
    }

    internal class CreateTeamCommandHandler : BaseHandler, IRequestHandler<CreateTeamCommand, Guid>
    {
        public CreateTeamCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var response = await HttpClient.PostAsJsonAsync("teams", request, cancellationToken);
            return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
        }
    }
}