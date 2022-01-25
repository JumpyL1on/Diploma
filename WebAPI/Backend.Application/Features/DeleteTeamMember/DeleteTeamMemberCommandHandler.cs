using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.DeleteTeamMember;

public class DeleteTeamMemberCommandHandler : BaseCommandHandler, IRequestHandler<DeleteTeamMemberCommand, Unit>
{
    public DeleteTeamMemberCommandHandler(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Unit> Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMember = await DbContext
            .Set<TeamMember>()
            .FirstAsync(x => x.AppUserId == request.AppUser.Id, cancellationToken);
        DbContext.Entry(teamMember).State = EntityState.Deleted;
        await DbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}