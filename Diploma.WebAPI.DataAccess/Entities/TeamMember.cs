namespace Diploma.WebAPI.DataAccess.Entities;

public class TeamMember
{
    public Guid Id { get; set; }
    public string Role { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
}