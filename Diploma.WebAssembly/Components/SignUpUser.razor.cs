using Diploma.Common.Enums;
using Diploma.Common.Interfaces;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class SignUpUser
{
    private MudForm MudForm { get; set; }
    private SignUpUserRequest Request { get; } = new();
    private bool IsPasswordVisible { get; set; }
    private InputType InputType { get; set; } = InputType.Password;
    private string InputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public ISnackbar Snackbar { get; set; }

    [Inject] public IUserService UserService { get; set; }
    [Inject] public IUserValidationService UserValidationService { get; set; }
    
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

    private async Task SignUpAsync()
    {
        var result = await UserService.SignUpAsync(Request);

        if (result.ResultType == ResultType.Ok)
        {
            NavigationManager.NavigateTo("user/sign-in");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Snackbar.Add(error, Severity.Error);
            }
        }
    }
}