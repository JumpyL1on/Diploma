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
                dto => dto.FullName,
                cfg => cfg.MapFrom(member => $@"{member.User.Name} {member.User.UserName} {member.User.Surname}"));
    }
}