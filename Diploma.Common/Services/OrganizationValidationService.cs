using Diploma.Common.Interfaces;

namespace Diploma.Common.Services;

public class OrganizationValidationService : IOrganizationValidationService
{
    public string? ValidateTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return "Не заполнено поле: Название";
        }

        return null;
    }
}