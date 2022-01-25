using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Base;

public abstract class BaseCommandHandler
{
    protected DbContext DbContext { get; }

    protected BaseCommandHandler(DbContext dbContext)
    {
        DbContext = dbContext;
    }
}