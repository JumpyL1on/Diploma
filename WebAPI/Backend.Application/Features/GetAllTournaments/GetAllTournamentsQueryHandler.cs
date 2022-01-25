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

namespace Backend.Application.Features.GetAllTournaments;

internal class GetAllTournamentsQueryHandler : BaseQueryHandler,
    IRequestHandler<GetAllTournamentsQuery, TournamentPreviewDTO[]>
{
    public GetAllTournamentsQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<TournamentPreviewDTO[]> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
    {
        return await DbContext
            .Set<Tournament>()
            .OrderByDescending(tournament => tournament.Creation)
            .ProjectTo<TournamentPreviewDTO>(Mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}