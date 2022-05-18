using System.Security.Claims;
using System.Text.Json;

namespace Diploma.WebAssembly.BusinessLogic.Services;

public static class JWTParser
{
    public static List<Claim> ParseClaimsFromJWT(string jwt)
    {
        var payload = jwt.Split('.')[1];
            
        var bytes = ParseBase64WithoutPadding(payload);
            
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);
            
        return dictionary
            .Select(pair => new Claim(pair.Key, pair.Value.ToString() ?? string.Empty))
            .ToList();
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}