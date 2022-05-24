using Diploma.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface ITeamMemberService
{
    public Task<Result<object>> CreateAsync(Guid userId, Guid teamId);
    public Task<Result<object>> InviteToLobby([FromServices] SteamGameClient gameClient, Guid userId);
    public Task<Result<object>> DeleteAsync(Guid id, Guid userId);
}