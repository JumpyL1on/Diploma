using Microsoft.AspNetCore.Identity;

namespace Diploma.WebAPI.DataAccess.Entities;

public class User : IdentityUser<Guid>
{
    public TeamMember TeamMember { get; set; }
    public OrganizationMember OrganizationMember { get; set; }
    public ICollection<UserGame> UserGames { get; set; }
}