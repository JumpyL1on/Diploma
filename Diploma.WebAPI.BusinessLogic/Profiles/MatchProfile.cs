using AutoMapper;
using Diploma.Common.DTOs;
using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Profiles;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<Match, MatchDTO>();
    }
}