using System;
using Backend.Core.Base;

namespace Backend.Core.Entities;

public class TeamMember : BaseEntity
{
    public virtual AppUser AppUser { get; set; }
    public Guid AppUserId { get; protected set; }
    public virtual Team Team { get; set; }
    public Guid TeamId { get; protected set; }
    public string Role { get; protected set; }

    public TeamMember(Guid appUserId, Guid teamId, string role)
    {
        AppUserId = appUserId;
        TeamId = teamId;
        Role = role;
    }
}