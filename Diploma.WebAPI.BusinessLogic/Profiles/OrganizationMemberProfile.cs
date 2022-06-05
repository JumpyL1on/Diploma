using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class OrganizationMemberProfile : Profile
{
    public OrganizationMemberProfile()
    {
        CreateMap<OrganizationMember, OrganizationMemberDTO>()
            .ForMember(
                x => x.Role,
                x => x.MapFrom(y => y.Role))
            .ForMember(
                x => x.Nickname,
                x => x.MapFrom(y => y.User.UserName));

        CreateMap<OrganizationMember, OrganizationDTO>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.OrganizationId))
            .ForMember(x => x.Role, x => x.MapFrom(y => y.Role))
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Organization.Title));
    }
}