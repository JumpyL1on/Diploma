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

namespace Backend.Application.Features.GetTournamentById
{
    internal class GetTournamentByIdQueryHandler : BaseQueryHandler,
        IRequestHandler<GetTournamentByIdQuery, TournamentDTO>
    {
        public GetTournamentByIdQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TournamentDTO> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            return await DbContext
                .Set<Tournament>()
                .Where(tournament => tournament.Id == request.Id)
                .ProjectTo<TournamentDTO>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}