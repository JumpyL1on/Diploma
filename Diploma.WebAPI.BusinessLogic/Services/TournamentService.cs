using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Exceptions;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.BusinessLogic.Steam;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class TournamentService : ITournamentService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly SteamGameClient _steamGameClient;

    public TournamentService(
        AppDbContext dbContext,
        IMapper mapper,
        IBackgroundJobClient backgroundJobClient,
        SteamGameClient steamGameClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _backgroundJobClient = backgroundJobClient;
        _steamGameClient = steamGameClient;
    }

    public async Task<List<TournamentDTO>> GetUpcomingTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => DateTime.UtcNow < tournament.Start)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetCurrentTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => DateTime.UtcNow >= tournament.Start && tournament.End == null)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetFinishedTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => tournament.End != null)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TournamentDetailsDTO> GetById(Guid id, Guid userId)
    {
        var tournament = await _dbContext.Tournaments
            .Where(tournament => tournament.Id == id)
            .ProjectTo<TournamentDetailsDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (tournament == null)
        {
            throw new NotFoundException("Турнира с таким идентификатором не существует");
        }
        
        tournament.IsRegistered = await _dbContext.Participants
            .Where(x => x.TournamentId == id)
            .AnyAsync(x => x.Team.TeamMembers.Any(y => y.UserId == userId));

        return tournament;
    }

    public async Task CreateTournamentAsync(CreateTournamentRequest request, Guid userId)
    {
        var gameId = await _dbContext.Games
            .Where(x => x.Title == request.Game)
            .Select(x => x.Id)
            .SingleOrDefaultAsync();

        var organizationId = await _dbContext.OrganizationMembers
            .Where(x => x.UserId == userId)
            .Select(x => x.OrganizationId)
            .SingleOrDefaultAsync();
        
        var tournament = new Tournament
        {
            Title = request.Title,
            MaxParticipantsNumber = request.MaxParticipantsNumber,
            CreatedAt = DateTime.UtcNow,
            GameId = gameId,
            OrganizationId = organizationId,
            Start = request.Start.ToUniversalTime().Add(request.TimeStart)
        };

        _dbContext.Tournaments.Add(tournament);

        await _dbContext.SaveChangesAsync();


        var id = tournament.Id;
        var start = tournament.Start;

        _backgroundJobClient
            .Schedule<ITournamentService>(
                tournamentService => tournamentService.BeginTournament(id, start, request.MaxParticipantsNumber),
                start - DateTime.UtcNow);
    }

    public async Task BeginTournament(Guid id, DateTime start, int participantNumber)
    {
        var participantIds = await _dbContext.Participants
            .Where(participant => participant.TournamentId == id)
            .Select(participant => participant.Id)
            .ToListAsync();

        var number = 1;

        var random = new Random();
        
        for (var i = 0; i < participantNumber / 2; i++)
        {
            var participantAId = GetRandomParticipantId(participantIds, random);

            var participantBId = GetRandomParticipantId(participantIds, random);

            var match = new Match
            {
                Id = Guid.NewGuid(),
                Start = start,
                Round = 1,
                Order = number++,
                ParticipantAId = participantAId,
                ParticipantBId = participantBId,
                TournamentId = id
            };

            _backgroundJobClient.Enqueue<IMatchService>(x => x.CreateAsync());
            
            var matchId = match.Id;
            
            _backgroundJobClient.Schedule<IMatchService>(
                x => x.StartAsync(matchId),
                TimeSpan.FromMinutes(2));
            
            _dbContext.Matches.Add(match);
        }
        
        participantNumber /= 2;
        for (var i = 2; participantNumber != 1; participantNumber /= 2)
        {
            var order = 1;
            for (var j = 0; j < participantNumber / 2; j++)
            {
                var match = new Match
                {
                    Start = null,
                    Round = i,
                    Order = order++,
                    ParticipantAId = null,
                    ParticipantBId = null,
                    TournamentId = id
                };

                _dbContext.Matches.Add(match);
            }

            i++;
        }

        await _dbContext.SaveChangesAsync();
    }

    private Guid GetRandomParticipantId(List<Guid> participantIds, Random random)
    {
        var participantId = participantIds[random.Next(participantIds.Count)];

        participantIds.Remove(participantId);

        return participantId;
    }
}