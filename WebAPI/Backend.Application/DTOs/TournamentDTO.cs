using System;
using Backend.Core.Enums;

namespace Backend.Application.DTOs;

public class TournamentDTO
{
    public string Title { get; set; }
    public TournamentStatus TournamentStatus { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public int CurrentParticipantsNumber { get; set; }
    public int MaxParticipantsNumber { get; set; }
}