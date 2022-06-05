using Diploma.Common.Interfaces;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Diploma.WebAssembly.Components.Dialogs;

public partial class CreateTournamentDialog
{
    private readonly CreateTournamentRequest _request = new();
    private MudForm _form = null!;
    private DateTime? _start;
    private TimeSpan? _timeStart;
    [CascadingParameter] public MudDialogInstance DialogInstance { get; set; } = null!;
    [Inject] public ITournamentService TournamentService { get; set; } = null!;
    [Inject] public ITournamentValidationService TournamentValidationService { get; set; } = null!;

    private async Task OnClickAsync()
    {
        await _form.Validate();

        if (_form.IsValid)
        {
            _request.Start = _start.GetValueOrDefault();

            _request.TimeStart = _timeStart.GetValueOrDefault();

            await TournamentService.CreateAsync(_request);

            DialogInstance.Close(DialogResult.Ok(true));
        }
    }
}