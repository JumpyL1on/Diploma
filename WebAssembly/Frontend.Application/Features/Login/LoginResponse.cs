namespace Frontend.Application.Features.Login
{
    public class LoginResponse
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}