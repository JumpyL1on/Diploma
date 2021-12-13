using System;
using Frontend.Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace Frontend.WebAssembly.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject] public HttpInterceptorService HttpInterceptorService { get; set; }

        public void Dispose() => HttpInterceptorService.DisposeEvent();

        protected override void OnInitialized() => HttpInterceptorService.RegisterEvent();
    }
}