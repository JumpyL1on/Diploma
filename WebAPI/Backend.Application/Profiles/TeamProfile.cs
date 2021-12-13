using AutoMapper;
using Backend.Application.DTOs;
using Backend.Core.Entities;

namespace Backend.Application.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDTO>();
        }
    }
}