using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.RefreshJWT
{
    public class RefreshJWTCommandHandler : BaseHandler, IRequestHandler<RefreshJWTCommand, Unit>
    {
        private ILocalStorageService LocalStorageService { get; }

        public RefreshJWTCommandHandler(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient)
        {
            LocalStorageService = localStorageService;
        }

        public async Task<Unit> Handle(RefreshJWTCommand request, CancellationToken cancellationToken)
        {
            request.Token = await LocalStorageService.GetItemAsStringAsync("token");
            request.RefreshToken = await LocalStorageService.GetItemAsStringAsync("refreshToken");
            var response = await HttpClient.PostAsJsonAsync("jwt", request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<RefreshJWTResponse>(cancellationToken: cancellationToken);
            await LocalStorageService.SetItemAsStringAsync("token", result?.Token);
            await LocalStorageService.SetItemAsStringAsync("refreshToken", result?.RefreshToken);
            return Unit.Value;
        }
    }
}