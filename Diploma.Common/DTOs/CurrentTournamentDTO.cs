namespace Diploma.Common.DTOs;

public class CurrentTournamentDTO
{
    public Guid Id { get; set; }
    public int MaxParticipantsNumber { get; set; }
    public MatchDTO[] Matches { get; set; }
}