namespace Diploma.Common.DTOs;

public class MatchDetailsDTO
{
    public string TournamentTitle { get; set; } = null!;
    public Guid TournamentId { get; set; }
    public string GameTitle { get; set; } = null!;
    public LeftTeamDTO LeftTeam { get; set; } = null!;
    public int LeftTeamScore { get; set; }
    public int Round { get; set; }
    public DateTime? FinishedAt { get; set; }
    public int RightTeamScore { get; set; }
    public RightTeamDTO RightTeam { get; set; } = null!;
}