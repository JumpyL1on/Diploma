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
                cfg => cfg.MapFrom(member => member.User.UserName));

        CreateMap<TeamMember, TeamDTO>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.TeamId))
            .ForMember(x => x.Role, x => x.MapFrom(y => y.Role))
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Team.Title))
            .ForMember(x => x.GameTitle, x => x.MapFrom(y => y.Team.Game.Title));
    }
}