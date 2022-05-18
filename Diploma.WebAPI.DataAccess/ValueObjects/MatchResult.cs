namespace Diploma.WebAPI.DataAccess.ValueObjects;

public class MatchResult
{
    public int ParticipantAScore { get; protected set; }
    public int ParticipantBScore { get; protected set; }

    public MatchResult(int participantAScore, int participantBScore)
    {
        ParticipantAScore = participantAScore;
        ParticipantBScore = participantBScore;
    }
}