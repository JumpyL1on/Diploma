using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Frontend.Application.Base;
using MediatR;

namespace Frontend.Application.Features.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, LoginResponse>
    {
        private ILocalStorageService LocalStorageService { get; }

        public LoginCommandHandler(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient)
        {
            LocalStorageService = localStorageService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await HttpClient.PostAsJsonAsync("account/login", request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancellationToken);
            if (result?.Succeeded != true)
                return result;
            await LocalStorageService.SetItemAsStringAsync("token", result.Token);
            await LocalStorageService.SetItemAsStringAsync("refreshToken", result.RefreshToken);
            return result;
        }
    }
}