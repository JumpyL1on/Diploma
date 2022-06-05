namespace Diploma.Common.DTOs;

public class MatchDTO
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    public int ParticipantAScore { get; set; }
    public int ParticipantBScore { get; set; }
    public ParticipantDTO ParticipantA { get; set; }
    public ParticipantDTO ParticipantB { get; set; }
}