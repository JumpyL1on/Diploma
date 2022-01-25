using Backend.Application.Base;

namespace Backend.Application.DTOs;

public class CurrentTournamentDTO : BaseDTO
{
    public int MaxParticipantsNumber { get; set; }
    public MatchDTO[] Matches { get; set; }
}