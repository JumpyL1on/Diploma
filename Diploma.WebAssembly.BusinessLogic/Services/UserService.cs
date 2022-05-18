using System.Net.Http.Json;
using Blazored.LocalStorage;
using Diploma.Common.Helpers;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class UserService : IUserService
{
    public UserService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        HttpClient = httpClient;
        LocalStorageService = localStorageService;
    }

    private HttpClient HttpClient { get; }
    private ILocalStorageService LocalStorageService { get; }
    
    public async Task<Result<object>> SignUpAsync(SignUpUserRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync("users/sign-up", request);
        
        if (response.IsSuccessStatusCode)
        {
            return new OkResult<object>(null);
        }

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        return new UnprocessableEntityResult<object>(errors);
    }

    public async Task<Result<string>> SignInAsync(SignInUserRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync("users/sign-in", request);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            
            await LocalStorageService.SetItemAsStringAsync("token", token);

            return new OkResult<string>("");
        }

        var errors = await response.Content.ReadFromJsonAsync<List<string>>();

        return new UnprocessableEntityResult<string>(errors);
    }

    public async Task SignOutAsync()
    {
        await LocalStorageService.RemoveItemAsync("token");
    }
}