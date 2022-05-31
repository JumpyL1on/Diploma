using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Shared;

public partial class MainLayout : IDisposable
{
    [Inject] public HttpInterceptor HttpInterceptor { get; set; }

    public void Dispose() => HttpInterceptor.DisposeEvent();

    protected override void OnInitialized()
    {
        HttpInterceptor.RegisterEvent();
    }
}