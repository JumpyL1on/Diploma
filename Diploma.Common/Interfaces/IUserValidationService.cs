namespace Diploma.Common.Interfaces;

public interface IUserValidationService
{
    public string? ValidateName(string name);
    public string? ValidateUserName(string userName);
    public string? ValidateSurname(string surname);
    public string? ValidateEmail(string email);
    public string? ValidatePassword(string password);
    public string? ValidateRepeatedPassword(string password, string repeatedPassword);
}