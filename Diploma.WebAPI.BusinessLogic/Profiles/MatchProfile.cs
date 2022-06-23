using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<Match, MatchDTO>()
            .ForMember(
                dto => dto.Id,
                cfg => cfg.MapFrom(match => match.Id))
            .ForMember(
                dto => dto.Start,
                cfg => cfg.MapFrom(match => match.Start))
            .ForMember(
                dto => dto.FinishedAt,
                cfg => cfg.MapFrom(match => match.FinishedAt))
            .ForMember(
                dto => dto.Round,
                cfg => cfg.MapFrom(match => match.Round))
            .ForMember(
                dto => dto.Order,
                cfg => cfg.MapFrom(match => match.Order))
            .ForMember(
                dto => dto.LeftTeamScore,
                cfg => cfg.MapFrom(match => match.LeftTeamScore))
            .ForMember(
                dto => dto.RightTeamScore,
                cfg => cfg.MapFrom(match => match.RightTeamScore))
            .ForMember(
                dto => dto.LeftTeamTitle,
                cfg => cfg.MapFrom(match => match.LeftTeam.Title))
            .ForMember(
                dto => dto.RightTeamTitle,
                cfg => cfg.MapFrom(match => match.RightTeam.Title))
            .ForMember(
                dto => dto.TournamentId,
                cfg => cfg.MapFrom(match => match.TournamentId))
            .ForMember(
                dto => dto.TournamentTitle,
                cfg => cfg.MapFrom(match => match.Tournament.Title));

        CreateMap<Match, MatchDetailsDTO>()
            .ForMember(
                dto => dto.TournamentTitle,
                cfg => cfg.MapFrom(match => match.Tournament.Title))
            .ForMember(
                dto => dto.TournamentId,
                cfg => cfg.MapFrom(match => match.Tournament.Id))
            .ForMember(
                dto => dto.GameTitle,
                cfg => cfg.MapFrom(match => match.Tournament.Game.Title))
            .ForMember(
                dto => dto.LeftTeam,
                cfg => cfg.MapFrom(match => match.LeftTeam))
            .ForMember(
                dto => dto.LeftTeamScore,
                cfg => cfg.MapFrom(match => match.LeftTeamScore))
            .ForMember(
                dto => dto.Round,
                cfg => cfg.MapFrom(match => match.Round))
            .ForMember(
                dto => dto.FinishedAt,
                cfg => cfg.MapFrom(match => match.FinishedAt))
            .ForMember(
                dto => dto.RightTeamScore,
                cfg => cfg.MapFrom(match => match.RightTeamScore))
            .ForMember(
                dto => dto.RightTeam,
                cfg => cfg.MapFrom(match => match.RightTeam));
    }
}