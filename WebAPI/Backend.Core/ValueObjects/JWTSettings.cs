namespace Backend.Core.ValueObjects;

public class JWTSettings
{
    public string SecurityKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public double ExpiryInMinutes { get; set; }
}