using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Steam;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly SteamGameClient _steamGameClient;

    public CurrentUserService(AppDbContext dbContext, IMapper mapper, SteamGameClient steamGameClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _steamGameClient = steamGameClient;
    }

    public async Task<List<TournamentDTO>> GetAllTournamentsAsync(Guid userId)
    {
        return await _dbContext.Tournaments
            .Where(x => x.Participants.Any(y => y.Team.TeamMembers.Any(z => z.UserId == userId)))
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TeamDTO>> GetAllTeamsAsync(Guid userId)
    {
        return await _dbContext.TeamMembers
            .Where(x => x.UserId == userId)
            .ProjectTo<TeamDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<GameDTO>> GetAllGamesAsync(Guid userId)
    {
        return await _dbContext.UserGames
            .Where(x => x.UserId == userId)
            .ProjectTo<GameDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<MatchDTO>> GetAllMatchesAsync(Guid userId)
    {
        return await _dbContext.Matches
            .Where(x => x.Tournament.Participants.Any(y => y.Team.TeamMembers.Any(z => z.UserId == userId)))
            .ProjectTo<MatchDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<OrganizationDTO>> GetAllOrganizationsAsync(Guid userId)
    {
        return await _dbContext.OrganizationMembers
            .Where(x => x.UserId == userId)
            .ProjectTo<OrganizationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task InviteToLobbyAsync(Guid userId)
    {
        var steamId = await _dbContext.UserGames
            .Where(x => x.UserId == userId)
            .Select(x => x.SteamId)
            .SingleOrDefaultAsync();

        _steamGameClient.InviteToLobby(steamId);
    }
}