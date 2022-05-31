using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, TeamDTO>()
            .ForMember(x => x.GameTitle, x => x.MapFrom(y => y.Game.Title));
    }
}