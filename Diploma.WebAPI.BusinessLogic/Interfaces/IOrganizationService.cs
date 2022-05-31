using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IOrganizationService
{
    public Task CreateAsync(CreateOrganizationRequest request, Guid userId);
}