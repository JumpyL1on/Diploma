using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Exceptions;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
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

    public TournamentService(
        AppDbContext dbContext,
        IMapper mapper,
        IBackgroundJobClient backgroundJobClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<List<TournamentDTO>> GetUpcomingTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => DateTime.UtcNow < tournament.Start)
            .OrderByDescending(tournament => tournament.Start)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetCurrentTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => DateTime.UtcNow >= tournament.Start && tournament.FinishedAt == null)
            .OrderByDescending(tournament => tournament.Start)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetFinishedTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => tournament.FinishedAt != null)
            .OrderByDescending(tournament => tournament.Start)
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

        tournament.IsRegistered = await _dbContext.TeamTournaments
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
        var teamIds = await _dbContext.TeamTournaments
            .Where(x => x.TournamentId == id)
            .Select(x => x.TeamId)
            .ToListAsync();

        var number = 1;

        var random = new Random();

        var matches = new List<Match>();

        for (var i = 0; i < participantNumber / 2; i++)
        {
            var leftTeamId = GetRandomTeamId(teamIds, random);

            var rightTeamId = GetRandomTeamId(teamIds, random);

            var match = new Match
            {
                Id = Guid.NewGuid(),
                Start = start,
                Round = 1,
                Order = number++,
                LeftTeamId = leftTeamId,
                RightTeamId = rightTeamId,
                TournamentId = id
            };

            matches.Add(match);
        }

        _dbContext.Matches.AddRange(matches);

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
                    LeftTeamId = null,
                    RightTeamId = null,
                    TournamentId = id
                };

                _dbContext.Matches.Add(match);
            }

            i++;
        }

        await _dbContext.SaveChangesAsync();

        foreach (var match in matches)
        {
            _backgroundJobClient.Enqueue<IMatchService>(x => x.CreateAsync(match.Id));
        }
    }

    private static Guid GetRandomTeamId(IList<Guid> teamIds, Random random)
    {
        var teamId = teamIds[random.Next(teamIds.Count)];

        teamIds.Remove(teamId);

        return teamId;
    }
}