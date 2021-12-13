using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Frontend.Infrastructure.Services
{
    public static class JwtParser
    {
        public static List<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var bytes = ParseBase64WithoutPadding(payload);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(bytes);
            claims.AddRange(dictionary?.Select(pair => new Claim(pair.Key, pair.Value.ToString() ?? string.Empty)) ??
                            Array.Empty<Claim>());
            return claims;
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
}