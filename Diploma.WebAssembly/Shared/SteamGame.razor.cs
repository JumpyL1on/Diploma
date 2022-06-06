using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Shared;

public partial class SteamGame
{
    [Parameter] public string GameTitle { get; set; } = null!;
}