using System;
using System.Collections.Generic;
using Backend.Core.Base;
using Backend.Core.Enums;

namespace Backend.Core.Entities;

public class Tournament : BaseEntity
{
    public string Title { get; protected set; }
    public TournamentStatus TournamentStatus { get; protected set; }
    public DateTime RegistrationStart { get; protected set; }
    public DateTime RegistrationEnd { get; protected set; }
    public DateTime Start { get; protected set; }
    public DateTime End { get; protected set; }
    public int CurrentParticipantsNumber { get; protected set; }
    public int MaxParticipantsNumber { get; protected set; }
    public DateTime Creation { get; protected set; }
    public virtual ICollection<Participant> Participants { get; protected set; }
    public virtual ICollection<Match> Matches { get; protected set; }

    public Tournament(string title, DateTime registrationStart, DateTime registrationEnd, DateTime start,
        DateTime end, int maxParticipantsNumber)
    {
        Title = title;
        TournamentStatus = TournamentStatus.Open;
        RegistrationStart = registrationStart;
        RegistrationEnd = registrationEnd;
        Start = start;
        End = end;
        MaxParticipantsNumber = maxParticipantsNumber;
        Creation = DateTime.Now;
    }

    public void JoinParticipant()
    {
        CurrentParticipantsNumber++;
    }
}