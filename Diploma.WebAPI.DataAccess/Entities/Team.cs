namespace Diploma.WebAPI.DataAccess.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public ICollection<TeamMember> TeamMembers { get; set; }
    public ICollection<TeamTournament> TeamTournaments { get; set; }
    public ICollection<Match> LeftTeamMatches { get; set; }
    public ICollection<Match> RightTeamMatches { get; set; }
}