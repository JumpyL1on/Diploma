namespace Diploma.WebAPI.DataAccess.Entities;

public class TeamTournament
{
    public int? AchievedPlace { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}