using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDetailsDTO>();
        CreateMap<Tournament, TournamentDTO>();
        CreateMap<Tournament, CurrentTournamentDTO>();
    }
}