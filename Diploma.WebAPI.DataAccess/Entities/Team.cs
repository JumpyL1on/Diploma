namespace Diploma.WebAPI.DataAccess.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid GameId { get; set; }
    public Game Game { get; set; } = null!;
    public ICollection<TeamMember> TeamMembers { get; set; } = null!;
    public ICollection<TeamTournament> TeamTournaments { get; set; } = null!;
    public ICollection<Match> LeftTeamMatches { get; set; } = null!;
    public ICollection<Match> RightTeamMatches { get; set; } = null!;
}