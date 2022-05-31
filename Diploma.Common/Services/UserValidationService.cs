using Diploma.Common.Interfaces;

namespace Diploma.Common.Services;

public class UserValidationService : IUserValidationService
{
    public string? ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return "Имя обязательно для заполнения";
        }

        return null;
    }

    public string? ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return "Никнейм обязателен для заполнения";
        }

        return null;
    }

    public string? ValidateSurname(string surname)
    {
        if (string.IsNullOrEmpty(surname))
        {
            return "Фамилия обязательна для заполнения";
        }

        return null;
    }

    public string? ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return "Электронная почта обязательна для заполнения";
        }

        return null;
    }

    public string? ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return "Пароль обязателен для заполнения";
        }

        return null;
    }

    public string? ValidateRepeatedPassword(string password, string repeatedPassword)
    {
        if (password != repeatedPassword)
        {
            return "Пароли не совпадают";
        }

        return null;
    }
}