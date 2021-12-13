using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Base
{
    public abstract class BaseHandler
    {
        protected DbContext DbContext { get; }

        protected BaseHandler(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}