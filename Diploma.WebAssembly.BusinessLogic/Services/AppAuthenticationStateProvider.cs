using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class AppAuthenticationStateProvider : AuthenticationStateProvider
{
    public AppAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        LocalStorage = localStorage;
        HttpClient = httpClient;
        Anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    private ILocalStorageService LocalStorage { get; }
    private AuthenticationState Anonymous { get; }
    private HttpClient HttpClient { get; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await LocalStorage.GetItemAsStringAsync("token");

        if (string.IsNullOrWhiteSpace(token))
        {
            return Anonymous;
        }

        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
        var claims = JWTParser.ParseClaimsFromJWT(token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }

    public async Task Notify()
    {
        var token = await LocalStorage.GetItemAsStringAsync("token");
        var state = new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaimsFromJWT(token), "jwt")));
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }
}