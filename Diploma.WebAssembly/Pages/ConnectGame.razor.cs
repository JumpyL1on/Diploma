using System.Net.Http.Json;
using Diploma.Common.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Diploma.WebAssembly.Pages;

public partial class ConnectGame
{
    [Inject] public NavigationManager NavManager { get; set; }

    [Inject] public HttpClient HttpClient { get; set; }

    private readonly CreateUserGameRequest _request = new();

    protected override async Task OnInitializedAsync()
    {
        var uri = NavManager.ToAbsoluteUri(NavManager.Uri);

        var queryStrings = QueryHelpers.ParseQuery(uri.Query);

        if (queryStrings.TryGetValue("claimedId", out var claimedId))
        {
            string str = claimedId;

            _request.SteamId = ulong.Parse(str[37..]);
        }

        if (queryStrings.TryGetValue("nickname", out var nickname))
        {
            _request.Nickname = nickname;
        }

        await HttpClient.PostAsJsonAsync("user-games", _request);

        NavManager.NavigateTo("/user/profile");
    }
}