using Diploma.Common.Interfaces;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class SignUpUser
{
    private MudForm _mudForm = null!;
    private readonly SignUpUserRequest _request = new();
    private bool _isPasswordVisible;
    private InputType _inputType = InputType.Password;
    private string _inputIcon = Icons.Material.Filled.VisibilityOff;
    [Inject] public IUserService UserService { get; set; } = null!;
    [Inject] public IUserValidationService UserValidationService { get; set; } = null!;
    [Inject] public NavigationManager NavManager { get; set; } = null!;

    private void Toggle()
    {
        if (_isPasswordVisible)
        {
            _isPasswordVisible = false;
            _inputType = InputType.Password;
            _inputIcon = Icons.Material.Filled.VisibilityOff;
        }
        else
        {
            _isPasswordVisible = true;
            _inputType = InputType.Text;
            _inputIcon = Icons.Material.Filled.Visibility;
        }
    }

    private async Task OnClickAsync()
    {
        await _mudForm.Validate();

        if (_mudForm.IsValid)
        {
            await UserService.SignUpUserAsync(_request);

            NavManager.NavigateTo("user/sign-in");
        }
    }
}