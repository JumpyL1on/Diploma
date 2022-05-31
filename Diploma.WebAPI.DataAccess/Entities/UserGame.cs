namespace Diploma.WebAPI.DataAccess.Entities;

public class UserGame
{
    public string Nickname { get; set; }
    public ulong SteamId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
}