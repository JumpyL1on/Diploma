namespace Diploma.WebAPI.DataAccess.Entities;

public class Match
{
    public Guid Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? FinishedAt { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    public int LeftTeamScore { get; set; }
    public Guid? LeftTeamId { get; set; }
    public Team LeftTeam { get; set; }
    public int RightTeamScore { get; set; }
    public Guid? RightTeamId { get; set; }
    public Team RightTeam { get; set; }
    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}