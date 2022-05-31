namespace Diploma.WebAPI.DataAccess.Entities;

public class OrganizationMember
{
    public Guid Id { get; set; }
    public string Role { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
}