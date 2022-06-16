using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly SteamGameClient.SteamGameClient _steamGameClient;

    public CurrentUserService(AppDbContext dbContext, IMapper mapper, SteamGameClient.SteamGameClient steamGameClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _steamGameClient = steamGameClient;
    }

    public async Task<List<TournamentDTO>> GetAllTournamentsAsync(Guid userId)
    {
        return await _dbContext.TeamTournaments
            .Where(teamTournament => teamTournament.Team.TeamMembers.Any(teamMember => teamMember.UserId == userId))
            .Select(teamTournament => teamTournament.Tournament)
            .OrderByDescending(tournament => tournament.Start)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TeamDTO>> GetAllTeamsAsync(Guid userId)
    {
        return await _dbContext.TeamMembers
            .Where(teamMember => teamMember.UserId == userId)
            .ProjectTo<TeamDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<UserGameDTO>> GetAllGamesAsync(Guid userId)
    {
        return await _dbContext.UserGames
            .Where(userGame => userGame.UserId == userId)
            .ProjectTo<UserGameDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<MatchDTO>> GetAllMatchesAsync(Guid userId)
    {
        return await _dbContext.Matches
            .Where(match => match.LeftTeam.TeamMembers.Any(teamMember => teamMember.UserId == userId) ||
                            match.RightTeam.TeamMembers.Any(teamMember => teamMember.UserId == userId))
            .ProjectTo<MatchDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<OrganizationDTO>> GetAllOrganizationsAsync(Guid userId)
    {
        return await _dbContext.OrganizationMembers
            .Where(organizationMember => organizationMember.UserId == userId)
            .ProjectTo<OrganizationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task InviteToLobbyAsync(Guid matchId, Guid userId)
    {
        var isLeftTeam = await _dbContext.Matches
            .Where(match => match.Id == matchId)
            .AnyAsync(match => match.LeftTeam.TeamMembers.Any(member => member.UserId == userId));

        var steamId = await _dbContext.UserGames
            .Where(x => x.UserId == userId)
            .Select(x => x.SteamId)
            .SingleOrDefaultAsync();

        _steamGameClient.InviteToLobby(steamId, isLeftTeam);
    }
}