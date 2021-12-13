using System;
using System.Collections.Generic;
using Backend.Core.Base;
using Backend.Core.Enums;

namespace Backend.Core.Entities
{
    public class Tournament : BaseEntity
    {
        public string Title { get; protected set; }
        public Status Status { get; protected set; }
        public DateTime RegistrationStart { get; protected set; }
        public DateTime RegistrationEnd { get; protected set; }
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
        public DateTime Creation { get; protected set; }
        public virtual ICollection<Participant> Participants { get; protected set; }
    }
}