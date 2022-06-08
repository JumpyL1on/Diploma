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
            .ForMember(
                dto => dto.Id,
                x => x.MapFrom(organizationMember => organizationMember.OrganizationId))
            .ForMember(
                dto => dto.Role,
                x => x.MapFrom(organizationMember => organizationMember.Role))
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(organizationMember => organizationMember.Organization.Title));
    }
}