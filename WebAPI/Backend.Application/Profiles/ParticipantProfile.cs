using AutoMapper;
using Backend.Application.DTOs;
using Backend.Core.Entities;

namespace Backend.Application.Profiles;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<Participant, ParticipantDTO>()
            .ForMember(dto => dto.TeamTitle, expression => expression.MapFrom(participant => participant.Team.Title));
    }
}