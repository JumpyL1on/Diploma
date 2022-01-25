using System;
using MediatR;

namespace Backend.Application.Features.CreateTournament;

public class CreateTournamentCommand : IRequest
{
    public string Title { get; set; }
    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public byte ParticipantsNumber { get; set; }
}