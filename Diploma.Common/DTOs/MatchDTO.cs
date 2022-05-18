namespace Diploma.Common.DTOs;

public class MatchDTO
{
    public Guid TournamentId { get; set; }
    public string TournamentTitle { get; set; }
    public ParticipantDTO ParticipantA { get; set; }
    public ParticipantDTO ParticipantB { get; set; }
    public int ParticipantAScore { get; set; }
    public int ParticipantBScore { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
}