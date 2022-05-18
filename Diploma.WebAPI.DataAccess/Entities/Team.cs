namespace Diploma.WebAPI.DataAccess.Entities;

public class Team
{
    public Team()
    {
        TeamMembers = new HashSet<TeamMember>();
        Participants = new HashSet<Participant>();
    }
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    public ICollection<TeamMember> TeamMembers { get; set; }
    public ICollection<Participant> Participants { get; set; }
}