using System.Threading.Tasks;
using Frontend.Application.Features.CreateTournament;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Frontend.WebAssembly.Pages;

public partial class CreateTournament
{
    [Inject] public IMediator Mediator { get; set; }
    private CreateTournamentCommand Request { get; } = new();
    private DateRange RegistrationDateRange { get; set; } = new();
    private DateRange DateRange { get; set; } = new();
    private bool IsError => DateRange != null && DateRange.Start == DateRange.End;

    private async Task CreateTournamentAsync()
    {
        Request.RegistrationStart = RegistrationDateRange.Start.GetValueOrDefault();
        Request.RegistrationEnd = RegistrationDateRange.End.GetValueOrDefault();
        Request.Start = DateRange.Start.GetValueOrDefault();
        Request.End = DateRange.End.GetValueOrDefault();
        await Mediator.Send(Request);
    }
}