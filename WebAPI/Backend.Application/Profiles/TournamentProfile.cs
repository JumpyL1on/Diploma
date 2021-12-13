using AutoMapper;
using Backend.Application.DTOs;
using Backend.Core.Entities;

namespace Backend.Application.Profiles
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentPreviewDTO>();
            CreateMap<Tournament, TournamentDTO>();
        }
    }
}