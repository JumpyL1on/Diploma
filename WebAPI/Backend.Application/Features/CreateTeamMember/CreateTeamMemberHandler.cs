using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Base;
using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.CreateTeamMember
{
    public class CreateTeamMemberHandler : BaseHandler,
        IRequestHandler<CreateTeamMemberCommand, CreateTeamMemberResponse>
    {
        private UserManager<AppUser> UserManager { get; }

        public CreateTeamMemberHandler(DbContext dbContext, UserManager<AppUser> userManager) : base(dbContext)
        {
            UserManager = userManager;
        }

        public async Task<CreateTeamMemberResponse> Handle(CreateTeamMemberCommand request,
            CancellationToken cancellationToken)
        {
            var teamMember = new TeamMember(request.AppUser.Id, request.TeamId, "Участник");
            DbContext.Entry(teamMember).State = EntityState.Added;
            await DbContext.SaveChangesAsync(cancellationToken);
            await UserManager.AddClaimAsync(request.AppUser, new Claim("Role", "Participant"));
            return new CreateTeamMemberResponse();
        }
    }
}