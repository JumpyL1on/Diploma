namespace Diploma.Common.Requests;

public class SignUpUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatedPassword { get; set; }
}