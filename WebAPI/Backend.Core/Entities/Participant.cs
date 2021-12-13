using System;
using Backend.Core.Base;

namespace Backend.Core.Entities
{
    public class Participant : BaseEntity
    {
        public virtual Team Team { get; protected set; }
        public Guid TeamId { get; protected set; }
        public virtual Tournament Tournament { get; protected set; }
        public Guid TournamentId { get; protected set; }
        public byte? AchievedPlace { get; protected set; }

        public Participant(Guid teamId, Guid tournamentId)
        {
            TeamId = teamId;
            TournamentId = tournamentId;
        }
    }
}