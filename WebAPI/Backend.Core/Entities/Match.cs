using System;
using Backend.Core.Base;

namespace Backend.Core.Entities;

public class Match : BaseEntity
{
    public DateTime Date { get; protected set; }
    public virtual Participant ParticipantA { get; protected set; }
    public Guid? ParticipantAId { get; protected set; }
    public virtual Participant ParticipantB { get; protected set; }
    public Guid? ParticipantBId { get; protected set; }
    public virtual Tournament Tournament { get; protected set; }
    public Guid TournamentId { get; protected set; }
    public int ParticipantAScore { get; protected set; }
    public int ParticipantBScore { get; protected set; }
    public int Order { get; protected set; }
    public int Position { get; protected set; }

    public Match(DateTime date, Guid? participantAId, Guid? participantBId, Guid tournamentId, int order, int position)
    {
        Date = date;
        ParticipantAId = participantAId;
        ParticipantBId = participantBId;
        TournamentId = tournamentId;
        Order = order;
        Position = position;
    }
}