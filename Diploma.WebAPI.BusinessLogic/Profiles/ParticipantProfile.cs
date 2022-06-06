using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<TeamTournament, ParticipantDTO>()
            .ForMember(
                x => x.TeamTitle,
                x => x.MapFrom(y => y.Team.Title))
            .ForMember(
                x => x.TeamMembers,
                x => x.MapFrom(y => y.Team.TeamMembers));
    }
}