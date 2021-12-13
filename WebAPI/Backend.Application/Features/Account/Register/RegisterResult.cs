using System.Collections.Generic;

namespace Backend.Application.Features.Account.Register
{
    public class RegisterResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}