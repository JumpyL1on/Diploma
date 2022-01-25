using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Application.Base;
using Backend.Application.DTOs;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.GetCurrentTournament;

internal class GetCurrentTournamentQueryHandler : BaseQueryHandler,
    IRequestHandler<GetCurrentTournamentQuery, CurrentTournamentDTO>
{
    public GetCurrentTournamentQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<CurrentTournamentDTO> Handle(GetCurrentTournamentQuery request,
        CancellationToken cancellationToken)
    {
        var teamMember = await DbContext
            .Set<TeamMember>()
            .SingleOrDefaultAsync(x => x.AppUserId == request.AppUser.Id, cancellationToken);
        var participant = await DbContext
            .Set<Participant>()
            .SingleOrDefaultAsync(x => x.TeamId == teamMember.TeamId, cancellationToken);
        return await DbContext
            .Set<Tournament>()
            .Where(x => x.Id == participant.TournamentId)
            .ProjectTo<CurrentTournamentDTO>(Mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);
    }
}