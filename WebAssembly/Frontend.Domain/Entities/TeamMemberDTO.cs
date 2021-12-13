using Frontend.Domain.Base;
using Frontend.Domain.ValueObjects;

namespace Frontend.Domain.Entities
{
    public class TeamMemberDTO : BaseDTO
    {
        public AppUserDTO AppUser { get; set; }
        public string Role { get; set; }
    }
}