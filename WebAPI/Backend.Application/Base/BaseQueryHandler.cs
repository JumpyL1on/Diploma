using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Base;

public abstract class BaseQueryHandler
{
    protected DbContext DbContext { get; }
    protected IMapper Mapper { get; }

    protected BaseQueryHandler(DbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }
}