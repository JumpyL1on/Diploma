using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<UserGame, GameDTO>()
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Game.Title))
            .ForMember(x => x.Nickname, x => x.MapFrom(y => y.Nickname));
    }
}