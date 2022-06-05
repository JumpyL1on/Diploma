namespace Diploma.Common.DTOs;

public class TournamentDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public int ParticipantsNumber { get; set; }
    public int MaxParticipantsNumber { get; set; }
}