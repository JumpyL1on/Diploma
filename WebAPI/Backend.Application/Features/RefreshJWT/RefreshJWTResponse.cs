namespace Backend.Application.Features.RefreshJWT
{
    public class RefreshJWTResponse
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}