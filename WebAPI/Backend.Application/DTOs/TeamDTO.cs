using System;
using Backend.Application.Base;

namespace Backend.Application.DTOs
{
    public class TeamDTO : BaseDTO
    {
        public string Title { get; set; }
        public Guid FounderId { get; set; }
        public TeamMemberDTO[] TeamMembers { get; set; }
        public bool Deletable { get; set; }
    }
}