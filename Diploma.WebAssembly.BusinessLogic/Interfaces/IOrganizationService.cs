using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IOrganizationService
{
    public Task CreateAsync(CreateOrganizationRequest request);
}