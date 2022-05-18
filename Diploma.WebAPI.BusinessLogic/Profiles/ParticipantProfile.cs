using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<Participant, ParticipantDTO>()
            .ForMember(dto => dto.TeamTitle, expression => expression.MapFrom(participant => participant.Team.Title));
    }
}