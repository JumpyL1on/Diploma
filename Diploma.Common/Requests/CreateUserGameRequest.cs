namespace Diploma.Common.Requests;

public class CreateUserGameRequest
{
    public string Nickname { get; set; }
    public ulong SteamId { get; set; }
}