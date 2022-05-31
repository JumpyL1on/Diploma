namespace Diploma.WebAPI.DataAccess.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<UserGame> UserSteamGames { get; set; }
    public ICollection<Team> Teams { get; set; }
}