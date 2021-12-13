using System;
using Frontend.Domain.Enums;

namespace Frontend.Domain.ValueObjects
{
    public class TournamentDTO
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime RegistrationEnd { get; set; }
    }
}