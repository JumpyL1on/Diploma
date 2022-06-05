using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<Organization, OrganizationDTO>();

        CreateMap<Organization, OrganizationDetailsDTO>();
    }
}