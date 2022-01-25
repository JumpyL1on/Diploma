using AutoMapper;
using Backend.Application.DTOs;
using Backend.Core.Entities;

namespace Backend.Application.Profiles;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<AppUser, AppUserDTO>();
    }
}