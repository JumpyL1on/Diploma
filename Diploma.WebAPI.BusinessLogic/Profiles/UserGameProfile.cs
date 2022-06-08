using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class UserGameProfile : Profile
{
    public UserGameProfile()
    {
        CreateMap<UserGame, UserGameDTO>()
            .ForMember(
                dto => dto.Nickname,
                cfg => cfg.MapFrom(y => y.Nickname))
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(y => y.Game.Title));
    }
}