using AutoMapper;
using Backend.Application.DTOs;
using Backend.Core.Entities;

namespace Backend.Application.Profiles;

public class TeamMemberProfile : Profile
{
    public TeamMemberProfile()
    {
        CreateMap<TeamMember, TeamMemberDTO>();
    }
}