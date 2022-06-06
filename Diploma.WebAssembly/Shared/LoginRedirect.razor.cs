using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Shared;

public partial class LoginRedirect
{
    [Inject] public NavigationManager NavManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        NavManager.NavigateTo("user/sign-in");
    }
}