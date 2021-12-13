using System.Collections.Generic;

namespace Frontend.Application.Features.Register
{
    public class RegisterResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}