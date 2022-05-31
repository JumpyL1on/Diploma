using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class MatchService : IMatchService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchDTO?> GetCurrentMatch(Guid userId)
    {
        return await _dbContext.Matches
            .Where(match =>
                match.End == null &&
                (match.ParticipantA.Team.TeamMembers.Any(teamMember => teamMember.UserId == userId) ||
                 match.ParticipantB.Team.TeamMembers.Any(teamMember => teamMember.UserId == userId)))
            .ProjectTo<MatchDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
}