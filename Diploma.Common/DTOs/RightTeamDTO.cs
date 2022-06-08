namespace Diploma.Common.DTOs;

public class RightTeamDTO
{
    public string Title { get; set; } = null!;
    public List<TeamMemberDTO> TeamMembers { get; set; } = null!;
}