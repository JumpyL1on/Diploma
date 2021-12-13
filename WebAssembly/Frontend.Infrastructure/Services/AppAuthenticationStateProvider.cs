using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Infrastructure.Services
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService LocalStorage { get; }
        private AuthenticationState Anonymous { get; }

        public AppAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
            Anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await LocalStorage.GetItemAsStringAsync("token");
            if (string.IsNullOrWhiteSpace(token))
                return Anonymous;
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt")));
        }

        public async Task Notify()
        {
            var token = await LocalStorage.GetItemAsStringAsync("token");
            var state = new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt")));
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
    }
}