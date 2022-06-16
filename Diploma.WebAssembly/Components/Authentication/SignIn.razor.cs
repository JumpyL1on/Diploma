using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components.Authentication;

public partial class SignIn
{
    [Inject] public IUserService UserService { get; set; }
    private readonly SignInUserRequest _request = new();
    private bool IsPasswordVisible { get; set; }
    private InputType InputType { get; set; } = InputType.Password;
    private string InputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

    private void Toggle()
    {
        if (IsPasswordVisible)
        {
            IsPasswordVisible = false;
            InputType = InputType.Password;
            InputIcon = Icons.Material.Filled.VisibilityOff;
        }
        else
        {
            IsPasswordVisible = true;
            InputType = InputType.Text;
            InputIcon = Icons.Material.Filled.Visibility;
        }
    }

    private async Task OnValidSubmitAsync()
    {
        await UserService.SignInUserAsync(_request);
    }
}