using System;
using Backend.Application.Base;
using Backend.Core.Enums;

namespace Backend.Application.DTOs
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