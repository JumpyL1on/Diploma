namespace Diploma.Common.Requests;

public class CreateTournamentRequest
{
    public string Title { get; set; }
    public string Game { get; set; } = "DOTA 2";
    public int MaxParticipantsNumber { get; set; } = 2;
    public DateTime Start { get; set; }
    public TimeSpan TimeStart { get; set; }
}