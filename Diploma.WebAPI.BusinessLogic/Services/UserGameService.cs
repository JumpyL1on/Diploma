using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class UserGameService : IUserGameService
{
    private readonly AppDbContext _dbContext;

    public UserGameService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(CreateUserGameRequest request, Guid userId)
    {
        var gameId = await _dbContext.Games
            .Where(x => x.Title == "DOTA 2")
            .Select(x => x.Id)
            .SingleOrDefaultAsync();

        var userGame = new UserGame
        {
            Nickname = request.Nickname,
            SteamId = request.SteamId,
            UserId = userId,
            GameId = gameId
        };

        _dbContext.UserGames.Add(userGame);

        await _dbContext.SaveChangesAsync();
    }
}