using System;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}