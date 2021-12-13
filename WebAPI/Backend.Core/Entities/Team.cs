using System.Collections.Generic;
using Backend.Core.Base;

namespace Backend.Core.Entities
{
    public class Team : BaseEntity
    {
        public string Title { get; protected set; }
        public virtual ICollection<TeamMember> TeamMembers { get; protected set; }

        public Team(string title)
        {
            Title = title;
        }
    }
}