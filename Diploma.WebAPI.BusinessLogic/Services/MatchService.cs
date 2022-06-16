using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Exceptions;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class MatchService : IMatchService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly SteamGameClient.SteamGameClient _steamGameClient;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public MatchService(
        AppDbContext dbContext,
        IMapper mapper,
        SteamGameClient.SteamGameClient steamGameClient,
        IBackgroundJobClient backgroundJobClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _steamGameClient = steamGameClient;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<List<MatchDTO>> GetAllByTournamentId(Guid id)
    {
        return await _dbContext.Matches
            .Where(x => x.TournamentId == id)
            .ProjectTo<MatchDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MatchDetailsDTO> GetById(Guid id, Guid userId)
    {
        var match = await _dbContext.Matches
            .Where(x => x.Id == id)
            .ProjectTo<MatchDetailsDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (match == null)
        {
            throw new NotFoundException("Матча с таким идентификатором не существует");
        }

        return match;
    }

    public async Task CreateAsync(Guid id)
    {
        _steamGameClient.CreateLobby(id, new CMsgPracticeLobbySetDetails
        {
            game_mode = (uint)DOTA_GameMode.DOTA_GAMEMODE_AP,
            game_name = "STEAMKIT2.DOTA.LOBBY",
            server_region = 3,
            allow_cheats = true,
            allchat = true,
            game_version = DOTAGameVersion.GAME_VERSION_CURRENT,
            visibility = DOTALobbyVisibility.DOTALobbyVisibility_Friends
        });

        _backgroundJobClient.Schedule<IMatchService>(
            x => x.StartAsync(id),
            TimeSpan.FromMinutes(2));
    }

    public async Task StartAsync(Guid id)
    {
        _steamGameClient.LaunchLobby();
    }
}