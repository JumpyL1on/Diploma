using System;
using Backend.Core.Enums;

namespace Backend.Application.DTOs
{
    public class TournamentDTO
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime RegistrationEnd { get; set; }
    }
}