using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Frontend.Application.Features.CreateTournament;

public class CreateTournamentCommand : IRequest
{
    [Required(ErrorMessage = "Название обязательно для заполнения")]
    public string Title { get; set; }

    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public byte ParticipantsNumber { get; set; }
}