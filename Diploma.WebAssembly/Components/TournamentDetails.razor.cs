using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class TournamentDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public ITournamentService TournamentService { get; set; } = null!;
    [Inject] public ITeamTournamentService TeamTournamentService { get; set; } = null!;
    private TournamentDetailsDTO? _tournament;

    private async Task OnClickAsync()
    {
        await TeamTournamentService.CreateAsync(Id);

        _tournament!.IsRegistered = true;
        
        Console.WriteLine(_tournament.IsRegistered);
    }

    protected override async Task OnInitializedAsync()
    {
        _tournament = await TournamentService.GetById(Id);
    }
}