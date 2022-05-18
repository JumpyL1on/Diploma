using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diploma.Common.DTOs;
using Diploma.Common.Helpers;
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

    public TournamentService(AppDbContext dbContext, IMapper mapper, IBackgroundJobClient backgroundJobClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<Result<List<TournamentDTO>>> GetAll()
    {
        var tournaments = await _dbContext.Tournaments
            .Where(tournament => tournament.Start < DateTime.Now)
            .ProjectTo<TournamentDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new OkResult<List<TournamentDTO>>(tournaments);
    }

    public async Task<Result<TournamentDetailsDTO>> GetById(Guid id)
    {
        var tournament = await _dbContext.Tournaments
            .Where(tournament => tournament.Id == id)
            .ProjectTo<TournamentDetailsDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        return new OkResult<TournamentDetailsDTO>(tournament);
    }

    public async Task<Result<object>> CreateTournamentAsync(CreateTournamentRequest request)
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
                tournamentService => tournamentService.BeginTournament(id, start),
                start - DateTime.UtcNow);

        return new CreatedResult<object>();
    }

    public async Task BeginTournament(Guid id, DateTime start)
    {
        var participantIds = await _dbContext.Participants
            .Where(participant => participant.TournamentId == id)
            .Select(participant => participant.Id)
            .ToListAsync();

        var number = 1;

        var random = new Random();

        for (var i = 0; i < 0; i++)
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

            _dbContext.Add(match);
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