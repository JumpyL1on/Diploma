using Backend.Application.Base;

namespace Backend.Application.DTOs
{
    public class TeamMemberDTO : BaseDTO
    {
        public AppUserDTO AppUser { get; set; }
        public string Role { get; set; }
    }
}