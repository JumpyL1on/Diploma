namespace Diploma.Common.DTOs;

public class MatchDTO
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public int Round { get; set; }
    public int Order { get; set; }
    public int LeftTeamScore { get; set; }
    public int RightTeamScore { get; set; }
    public string? LeftTeamTitle { get; set; }
    public string? RightTeamTitle { get; set; }
}