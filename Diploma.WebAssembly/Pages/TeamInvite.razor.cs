using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Pages;

public partial class TeamInvite
{
    [Parameter] public Guid Id { get; set; }

    [Inject] ITeamMemberService TeamMemberService { get; set; }

    [Inject] NavigationManager NavManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await TeamMemberService.CreateAsync(Id);

        NavManager.NavigateTo("/user/profile");
    }
}