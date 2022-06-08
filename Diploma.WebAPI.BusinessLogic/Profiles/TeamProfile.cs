using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, TeamDTO>()
            .ForMember(
                x => x.GameTitle,
                x => x.MapFrom(y => y.Game.Title));

        CreateMap<Team, TeamDetailsDTO>()
            .ForMember(
                x => x.GameTitle,
                x => x.MapFrom(y => y.Game.Title));

        CreateMap<Team, LeftTeamDTO>()
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(team => team.Title))
            .ForMember(
                dto => dto.TeamMembers,
                cfg => cfg.MapFrom(team => team.TeamMembers));
        
        CreateMap<Team, RightTeamDTO>()
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(team => team.Title))
            .ForMember(
                dto => dto.TeamMembers,
                cfg => cfg.MapFrom(team => team.TeamMembers));
    }
}