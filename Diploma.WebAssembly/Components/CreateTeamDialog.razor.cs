using Diploma.Common.Interfaces;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components;

public partial class CreateTeamDialog
{
    [CascadingParameter] public MudDialogInstance MudDialogInstance { get; set; }
    [Inject] public ITeamService TeamService { get; set; }
    [Inject] public ITeamValidationService TeamValidationService { get; set; }
    private CreateTeamRequest Request { get; } = new();
    private MudForm MudForm { get; set; }

    private async Task OnValidSubmitAsync()
    {
        await TeamService.CreateAsync(Request);
        
        MudDialogInstance.Close(DialogResult.Ok(true));
    }
}