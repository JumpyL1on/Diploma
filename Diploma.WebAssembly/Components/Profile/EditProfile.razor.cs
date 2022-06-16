using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.Components.Profile;

public partial class EditProfile
{
    [Inject] public HttpClient HttpClient { get; set; }

    private async Task OnClickAsync()
    {
        
    }
}