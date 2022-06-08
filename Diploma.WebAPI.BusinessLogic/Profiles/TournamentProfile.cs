using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDTO>()
            .ForMember(
                dto => dto.Id,
                cfg => cfg.MapFrom(tournament => tournament.Id))
            .ForMember(
                dto => dto.Title,
                cfg => cfg.MapFrom(tournament => tournament.Title))
            .ForMember(
                dto => dto.Start,
                cfg => cfg.MapFrom(tournament => tournament.Start))
            .ForMember(
                dto => dto.FinishedAt,
                cfg => cfg.MapFrom(tournament => tournament.FinishedAt))
            .ForMember(
                dto => dto.ParticipantsNumber,
                cfg => cfg.MapFrom(tournament => tournament.ParticipantsNumber))
            .ForMember(
                dto => dto.MaxParticipantsNumber,
                cfg => cfg.MapFrom(tournament => tournament.MaxParticipantsNumber));

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
                dto => dto.FinishedAt,
                cfg => cfg.MapFrom(tournament => tournament.FinishedAt))
            .ForMember(
                x => x.IsRegistered,
                x => x.Ignore());
    }
}