namespace Diploma.WebAPI.DataAccess.Entities;

public class Participant
{
    public Guid Id { get; set; }
    public int? AchievedPlace { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; }
    public ICollection<Match> ParticipantAMatches { get; set; }
    public ICollection<Match> ParticipantBMatches { get; set; }
}