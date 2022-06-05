using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<Match, MatchDTO>();

        CreateMap<Match, MatchDetailsDTO>()
            .ForMember(
                x => x.GameTitle,
                x => x.MapFrom(y => y.Tournament.Game.Title))
            .ForMember(
                x => x.TournamentTitle,
                x => x.MapFrom(y => y.Tournament.Title))
            .ForMember(
                x => x.TournamentId,
                x => x.MapFrom(y => y.TournamentId));
    }
}