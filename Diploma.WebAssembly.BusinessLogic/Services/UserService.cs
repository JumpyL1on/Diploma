using System.Net.Http.Json;
using Blazored.LocalStorage;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly NavigationManager _navManager;

    public UserService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navManager)
    {
        HttpClient = httpClient;
        LocalStorage = localStorage;
        _navManager = navManager;
    }

    private HttpClient HttpClient { get; }
    private ILocalStorageService LocalStorage { get; }
    
    public async Task SignUpUserAsync(SignUpUserRequest request)
    {
        await HttpClient.PostAsJsonAsync("users/sign-up", request);
    }

    public async Task SignInUserAsync(SignInUserRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync("users/sign-in", request);

        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();

            await LocalStorage.SetItemAsStringAsync("token", token);

            _navManager.NavigateTo("/", true);
        }
    }

    public async Task SignOutUserAsync()
    {
        await LocalStorage.RemoveItemAsync("token");
    }
}