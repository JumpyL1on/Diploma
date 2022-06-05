using Diploma.Common.Interfaces;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components.Dialogs;

public partial class CreateOrganizationDialog
{
    private readonly CreateOrganizationRequest _request = new();
    private MudForm _form = null!;
    [CascadingParameter] public MudDialogInstance DialogInstance { get; set; } = null!;
    [Inject] public IOrganizationService OrganizationService { get; set; } = null!;
    [Inject] public IOrganizationValidationService OrganizationValidationService { get; set; } = null!;

    private async Task OnClickAsync()
    {
        await _form.Validate();

        if (_form.IsValid)
        {
            await OrganizationService.CreateAsync(_request);

            DialogInstance.Close(DialogResult.Ok(true));
        }
    }
}