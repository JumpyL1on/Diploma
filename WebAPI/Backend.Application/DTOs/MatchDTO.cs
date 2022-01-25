namespace Backend.Application.DTOs;

public class MatchDTO
{
    public ParticipantDTO ParticipantA { get; set; }
    public ParticipantDTO ParticipantB { get; set; }
    public int ParticipantAScore { get; protected set; }
    public int ParticipantBScore { get; protected set; }
    public int Order { get; set; }
    public int Position { get; set; }
}