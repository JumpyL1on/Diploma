using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDTO>();

        CreateMap<Tournament, TournamentDetailsDTO>()
            .ForMember(
                x => x.Title,
                x => x.MapFrom(y => y.Title))
            .ForMember(
                x => x.GameTitle,
                x => x.MapFrom(y => y.Game.Title))
            .ForMember(
                x => x.OrganizationTitle,
                x => x.MapFrom(y => y.Organization.Title))
            .ForMember(
                x => x.ParticipantsNumber,
                x => x.MapFrom(y => y.ParticipantsNumber))
            .ForMember(
                x => x.MaxParticipantsNumber,
                x => x.MapFrom(y => y.MaxParticipantsNumber))
            .ForMember(
                x => x.IsRegistered,
                x => x.Ignore());
    }
}