using System;
using Frontend.Domain.Enums;

namespace Frontend.Domain.ValueObjects
{
    public class TournamentDTO
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime RegistrationEnd { get; set; }
        public int CurrentParticipantsNumber { get; set; }
        public int MaxParticipantsNumber { get; set; }
    }
}