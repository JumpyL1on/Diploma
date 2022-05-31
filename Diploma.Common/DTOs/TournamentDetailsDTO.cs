namespace Diploma.Common.DTOs;

public class TournamentDetailsDTO
{
    public string Title { get; set; }
    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
    public int CurrentParticipantsNumber { get; set; }
    public int ParticipantsNumber { get; set; }
}