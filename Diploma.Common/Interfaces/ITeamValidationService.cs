namespace Diploma.Common.Interfaces;

public interface ITeamValidationService
{
    public string? ValidateTitle(string title);
    public string? ValidateTag(string tag);
}