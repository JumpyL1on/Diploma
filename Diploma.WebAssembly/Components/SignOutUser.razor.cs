using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class SignOutUser
{
    [Inject] public IUserService UserService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await UserService.SignOutAsync();
        NavigationManager.NavigateTo("/", true);
    }
}