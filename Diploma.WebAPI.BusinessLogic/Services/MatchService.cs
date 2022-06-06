using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Exceptions;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Steam;
using Diploma.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using SteamKit2.GC.Dota.Internal;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class MatchService : IMatchService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly SteamGameClient _steamGameClient;

    public MatchService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        //_steamGameClient = steamGameClient;
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

    public async Task CreateAsync()
    {
        _steamGameClient.CreateLobby("password", new CMsgPracticeLobbySetDetails
        {
            game_mode = (uint)DOTA_GameMode.DOTA_GAMEMODE_AP,
            game_name = "STEAMKIT2.DOTA.LOBBY",
            server_region = 3,
            allow_cheats = true,
            allchat = true,
            game_version = DOTAGameVersion.GAME_VERSION_CURRENT,
        });
    }

    public async Task StartAsync(Guid id)
    {
        _steamGameClient.MatchId = id;

        _steamGameClient.LaunchLobby();
    }
}