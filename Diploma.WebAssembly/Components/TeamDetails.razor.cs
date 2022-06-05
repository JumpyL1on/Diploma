using Diploma.Common.DTOs;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components;

public partial class TeamDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public ITeamService TeamService { get; set; } = null!;
    private TeamDetailsDTO? _team;
    private List<(TeamMemberDTO?, int)> _teamMembers = null!;

    protected override async Task OnInitializedAsync()
    {
        _team = await TeamService.GetByIdAsync(Id);
        
        var count = _team.TeamMembers.Count;
        for (var i = 0; i < 5 - count; i++)
        {
            _team.TeamMembers.Add(null);
        }

        _teamMembers = _team.TeamMembers
            .Zip(Enumerable.Range(1, 5))
            .ToList();
    }
}