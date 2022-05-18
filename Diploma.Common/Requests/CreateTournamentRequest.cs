namespace Diploma.Common.Requests;

public class CreateTournamentRequest
{
    public string Title { get; set; }
    public int MaxParticipantsNumber { get; set; }
    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
}