namespace Backend.Application.Features.Account.Login
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}