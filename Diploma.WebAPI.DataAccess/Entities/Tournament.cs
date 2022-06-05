namespace Diploma.WebAPI.DataAccess.Entities;

public class Tournament
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int ParticipantsNumber { get; set; }
    public int MaxParticipantsNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public ICollection<Participant> Participants { get; set; }
    public ICollection<Match> Matches { get; set; }
}