using Diploma.Common.Interfaces;

namespace Diploma.Common.ValidationServices;

public class TeamValidationService : ITeamValidationService
{
    public string? ValidateTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return "Название обязательно для заполнения";
        }

        return null;
    }

    public string? ValidateTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            return "Тэг обязателен для заполнения";
        }

        return null;
    }
}