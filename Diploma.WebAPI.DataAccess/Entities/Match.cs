namespace Diploma.WebAPI.DataAccess.Entities;

public class Match
{
    public Guid Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    public int ParticipantAScore { get; set; }
    public int ParticipantBScore { get; set; }
    public Guid? ParticipantAId { get; set; }
    public Participant ParticipantA { get; set; }
    public Guid? ParticipantBId { get; set; }
    public Participant ParticipantB { get; set; }
    public Guid TournamentId { get; set; }
    public Tournament Tournament { get; set; }
}