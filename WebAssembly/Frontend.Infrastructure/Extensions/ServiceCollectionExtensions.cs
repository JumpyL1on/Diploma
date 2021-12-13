using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Frontend.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Frontend.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<HttpInterceptorService>();
            services.AddScoped(provider =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:13671/api/")
                };
                client.EnableIntercept(provider);
                return client;
            });
            services.AddHttpClientInterceptor();
            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();
        }
    }
}