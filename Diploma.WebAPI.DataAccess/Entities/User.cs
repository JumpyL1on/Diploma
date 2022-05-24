using Microsoft.AspNetCore.Identity;

namespace Diploma.WebAPI.DataAccess.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ulong? SteamId { get; set; }
    public TeamMember TeamMember { get; set; }
}