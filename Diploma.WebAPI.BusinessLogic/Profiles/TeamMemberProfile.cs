using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TeamMemberProfile : Profile
{
    public TeamMemberProfile()
    {
        CreateMap<TeamMember, TeamMemberDTO>()
            .ForMember(
                dto => dto.Nickname,
                cfg => cfg.MapFrom(teamMember => teamMember.User.UserName))
            .ForMember(
                dto => dto.Role,
                cfg => cfg.MapFrom(teamMember => teamMember.Role));

        CreateMap<TeamMember, TeamDTO>()
            .ForMember(
                dto => dto.Id,
                cfg => cfg.MapFrom(teamMember => teamMember.TeamId))
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(teamMember => teamMember.Team.Title))
            .ForMember(
                dto => dto.GameTitle,
                cfg => cfg.MapFrom(teamMember => teamMember.Team.Game.Title))
            .ForMember(
                dto => dto.Role,
                cfg => cfg.MapFrom(teamMember => teamMember.Role));
    }
}