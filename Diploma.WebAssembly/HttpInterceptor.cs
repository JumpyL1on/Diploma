using System.Net;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Toolbelt.Blazor;

namespace Diploma.WebAssembly;

public class HttpInterceptor
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly ISnackbar _snackbar;
    private readonly NavigationManager _navManager;

    public HttpInterceptor(
        HttpClientInterceptor interceptor,
        ISnackbar snackbar,
        NavigationManager navManager)
    {
        _interceptor = interceptor;
        _snackbar = snackbar;
        _navManager = navManager;
    }

    public void RegisterEvent() => _interceptor.AfterSendAsync += InterceptResponse;

    private async Task InterceptResponse(object? sender, HttpClientInterceptorEventArgs e)
    {
        if (!e.Response.IsSuccessStatusCode)
        {
            switch (e.Response.StatusCode)
            {
                case HttpStatusCode.Redirect:
                    _navManager.NavigateTo(await e.Response.Content.ReadAsStringAsync());
                    
                    break;
                case HttpStatusCode.BadRequest:
                    var error = await e.Response.Content.ReadAsStringAsync();

                    _snackbar.Add(error, Severity.Error);

                    break;
            }
        }
    }

    public void DisposeEvent() => _interceptor.AfterSendAsync -= InterceptResponse;
}