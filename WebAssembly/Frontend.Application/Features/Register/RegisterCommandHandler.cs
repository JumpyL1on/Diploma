using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.Register
{
    public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommand, RegisterResponse>
    {
        public RegisterCommandHandler(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await HttpClient.PostAsJsonAsync("account/register", request, cancellationToken);
            return await response.Content.ReadFromJsonAsync<RegisterResponse>(cancellationToken: cancellationToken);
        }
    }
}