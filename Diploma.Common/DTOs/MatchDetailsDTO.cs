namespace Diploma.Common.DTOs;

public class MatchDetailsDTO
{
    public Guid TournamentId { get; set; }
    public string TournamentTitle { get; set; }
    public string GameTitle { get; set; }
    public int Round { get; set; }
    public int ParticipantAScore { get; set; }
    public int ParticipantBScore { get; set; }
    public ParticipantDTO ParticipantA { get; set; }
    public ParticipantDTO ParticipantB { get; set; }
}