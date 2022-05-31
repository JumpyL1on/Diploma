namespace Diploma.WebAPI.DataAccess.Entities;

public class Organization
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<OrganizationMember> OrganizationMembers { get; set; }
    public ICollection<Tournament> Tournaments { get; set; }
}