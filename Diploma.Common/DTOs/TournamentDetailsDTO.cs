namespace Diploma.Common.DTOs;

public class TournamentDetailsDTO
{
    public string Title { get; set; }
    public string GameTitle { get; set; }
    public string OrganizationTitle { get; set; }
    public int ParticipantsNumber { get; set; }
    public int MaxParticipantsNumber { get; set; }
    public DateTime? FinishedAt { get; set; }
    public bool IsRegistered { get; set; }
}