using System.Net.Http.Json;
using Diploma.Common.Requests;
using Diploma.WebAssembly.BusinessLogic.Interfaces;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public class OrganizationService : IOrganizationService
{
    private readonly HttpClient _httpClient;

    public OrganizationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAsync(CreateOrganizationRequest request)
    {
        await _httpClient.PostAsJsonAsync("organizations", request);
    }
}