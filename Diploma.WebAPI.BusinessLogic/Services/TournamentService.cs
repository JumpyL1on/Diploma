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

    public TournamentService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        //_backgroundJobClient = backgroundJobClient;
    }

    public async Task<List<TournamentDTO>> GetUpcomingTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => tournament.Start < DateTime.UtcNow)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetCurrentTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => tournament.Start >= DateTime.UtcNow && tournament.End == null)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<TournamentDTO>> GetFinishedTournaments()
    {
        return await _dbContext.Tournaments
            .Where(tournament => tournament.End == null)
            .OrderByDescending(tournament => tournament.CreatedAt)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TournamentDetailsDTO> GetById(Guid id)
    {
        var tournament = await _dbContext.Tournaments
            .Where(tournament => tournament.Id == id)
            .ProjectTo<TournamentDetailsDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (tournament == null)
        {
            throw new NotFoundException("Турнира с таким идентификатором не существует");
        }

        return tournament;
    }

    public async Task CreateTournamentAsync(CreateTournamentRequest request)
    {
        var tournament = new Tournament
        {
            Title = request.Title,
            MaxParticipantsNumber = request.MaxParticipantsNumber,
            CreatedAt = DateTime.UtcNow,
            RegistrationStart = request.RegistrationStart,
            RegistrationEnd = request.RegistrationEnd,
            Start = request.Start
        };

        _dbContext.Tournaments.Add(tournament);

        await _dbContext.SaveChangesAsync();


        var id = tournament.Id;
        var start = tournament.Start;

        _backgroundJobClient
            .Schedule<ITournamentService>(
                tournamentService => tournamentService.BeginTournament(id, start, request.MaxParticipantsNumber),
                start - DateTime.Now);
    }

    public async Task BeginTournament(Guid id, DateTime start, int participantNumber)
    {
        var participantIds = await _dbContext.Participants
            .Where(participant => participant.TournamentId == id)
            .Select(participant => participant.Id)
            .ToListAsync();

        var number = 1;

        var random = new Random();

        var matches = new List<Match>();
        
        for (var i = 0; i < participantNumber / 2; i++)
        {
            var participantAId = GetRandomParticipantId(participantIds, random);

            var participantBId = GetRandomParticipantId(participantIds, random);

            var match = new Match
            {
                Start = start,
                Round = 1,
                Order = number++,
                ParticipantAId = participantAId,
                ParticipantBId = participantBId
            };
            
            matches.Add(match);

            _dbContext.Matches.Add(match);
        }

        await _dbContext.SaveChangesAsync();

        foreach (var match in matches)
        {
            
        }
        
        participantNumber /= 2;
        for (var i = 2; participantNumber != 1; participantNumber /= 2)
        {
            var order = 1;
            for (var j = 0; j < participantNumber / 2; j++)
            {
                var match = new Match
                {
                    Start = start,
                    Round = i,
                    Order = order++,
                    ParticipantAId = null,
                    ParticipantBId = null
                };

                _dbContext.Add(match);
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