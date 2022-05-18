namespace Diploma.WebAPI.DataAccess.ValueObjects;

public class LobbyDetails
{
    public string Title { get; protected set; }
    public string Password { get; protected set; }

    public LobbyDetails(string title, string password)
    {
        Title = title;
        Password = password;
    }
}