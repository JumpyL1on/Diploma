using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class UserGameProfile : Profile
{
    public UserGameProfile()
    {
        CreateMap<UserGame, GameDTO>()
            .ForMember(x => x.Nickname, x => x.MapFrom(y => y.Nickname))
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Game.Title));
    }
}