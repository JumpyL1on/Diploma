using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.CreateTeam
{
    public class CreateTeamCommandHandler : BaseHandler, IRequestHandler<CreateTeamCommand, Guid>
    {
        private UserManager<AppUser> UserManager { get; }

        public CreateTeamCommandHandler(DbContext dbContext, UserManager<AppUser> userManager) : base(dbContext)
        {
            UserManager = userManager;
        }

        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team(request.Title);
            DbContext.Entry(team).State = EntityState.Added;
            await DbContext.SaveChangesAsync(cancellationToken);
            var teamMember = new TeamMember(request.AppUser.Id, team.Id, "Владелец");
            DbContext.Entry(teamMember).State = EntityState.Added;
            await DbContext.SaveChangesAsync(cancellationToken);
            await UserManager.AddClaimAsync(request.AppUser, new Claim("Role", "Owner"));
            await DbContext.SaveChangesAsync(cancellationToken);
            return team.Id;
        }
    }
}