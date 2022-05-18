using Diploma.Common.Enums;

namespace Diploma.Common.DTOs;

public class TournamentDetailsDTO
{
    public string Title { get; set; }
    public TournamentStatus TournamentStatus { get; set; }
    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
    public int CurrentParticipantsNumber { get; set; }
    public int ParticipantsNumber { get; set; }
}