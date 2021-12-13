using System;
using Frontend.Domain.Base;
using Frontend.Domain.Enums;

namespace Frontend.Domain.Entities
{
    public class TournamentPreviewDTO : BaseDTO
    {
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime RegistrationStart { get; set; }
        public DateTime RegistrationEnd { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}