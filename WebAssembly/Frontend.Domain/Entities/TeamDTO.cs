using Frontend.Domain.Base;

namespace Frontend.Domain.Entities
{
    public class TeamDTO : BaseDTO
    {
        public string Title { get; set; }
        public TeamMemberDTO[] TeamMembers { get; set; }
    }
}