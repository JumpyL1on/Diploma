using Backend.Application.DTOs;
using Frontend.Domain.Base;

namespace Frontend.Domain.Entities;

public class CurrentTournamentDTO : BaseDTO
{
    public int MaxParticipantsNumber { get; set; }
    public MatchDTO[] Matches { get; set; }
}