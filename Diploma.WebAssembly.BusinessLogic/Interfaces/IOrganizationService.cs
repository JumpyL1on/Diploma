using Diploma.Common.DTOs;
using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IOrganizationService
{
    public Task<OrganizationDetailsDTO> GetByIdAsync(Guid id);
    public Task CreateAsync(CreateOrganizationRequest request);
}