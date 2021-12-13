using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Frontend.Application.Features.RefreshJWT;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Toolbelt.Blazor;

namespace Frontend.Infrastructure.Services
{
    public class HttpInterceptorService
    {
        private HttpClientInterceptor Interceptor { get; }
        private AuthenticationStateProvider AuthenticationStateProvider { get; }
        private IMediator Mediator { get; }
        private ILocalStorageService LocalStorageService { get; }

        public HttpInterceptorService(HttpClientInterceptor interceptor,
            AuthenticationStateProvider authenticationStateProvider, IMediator mediator,
            ILocalStorageService localStorageService)
        {
            Interceptor = interceptor;
            AuthenticationStateProvider = authenticationStateProvider;
            Mediator = mediator;
            LocalStorageService = localStorageService;
        }

        public void RegisterEvent() => Interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

        public void DisposeEvent() => Interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;

        private async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs args)
        {
            var path = args.Request.RequestUri?.AbsolutePath;
            if (!path.Contains("jwt") && !path.Contains("account"))
            {
                var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var exp = state.User.FindFirst("exp")?.Value;
                var time = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
                var diff = time - DateTimeOffset.Now;
                if (diff.TotalMinutes <= 0)
                    await Mediator.Send(new RefreshJWTCommand());
                args.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer",
                    await LocalStorageService.GetItemAsStringAsync("token"));
            }
        }
    }
}