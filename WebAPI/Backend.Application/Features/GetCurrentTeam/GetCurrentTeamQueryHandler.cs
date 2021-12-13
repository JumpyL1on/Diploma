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

namespace Backend.Application.Features.GetCurrentTeam
{
    public class GetCurrentTeamQueryHandler : BaseQueryHandler, IRequestHandler<GetCurrentTeamQuery, TeamDTO>
    {
        public GetCurrentTeamQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TeamDTO> Handle(GetCurrentTeamQuery request,
            CancellationToken cancellationToken)
        {
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(x => x.AppUserId == request.AppUser.Id)
                .FirstOrDefaultAsync(cancellationToken);
            return await DbContext
                .Set<Team>()
                .Where(x => x.Id == teamMember.TeamId)
                .ProjectTo<TeamDTO>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}