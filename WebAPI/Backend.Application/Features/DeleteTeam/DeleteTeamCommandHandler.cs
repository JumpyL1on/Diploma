using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.DeleteTeam
{
    public class DeleteTeamCommandHandler : BaseHandler, IRequestHandler<DeleteTeamCommand, Unit>
    {
        public DeleteTeamCommandHandler(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await DbContext
                .Set<Team>()
                .FindAsync(new object[] { request.Id }, cancellationToken);
            DbContext.Entry(team).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}