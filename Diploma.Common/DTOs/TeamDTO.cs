namespace Diploma.Common.DTOs;

public class TeamDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid FounderId { get; set; }
    public List<TeamMemberDTO> TeamMembers { get; set; }
    public bool Deletable { get; set; }
}