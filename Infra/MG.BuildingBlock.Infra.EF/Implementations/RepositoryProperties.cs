using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Domain.SeedWork;
using MG.BuildingBlock.Infra.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace MG.BuildingBlock.Infra.EF.Implementations;

public class RepositoryProperties<TEntity>(
    BaseContext dbContext
    ) where TEntity : Entity
{
    protected readonly BaseContext _dbContext = dbContext;

    protected DbSet<TEntity> Set => _dbContext.Set<TEntity>();

    protected IQueryable<TEntity> SetAsNoTracking
    {
        get
        {
            var query = Set.AsNoTracking();

            if (typeof(TEntity).IsSubclassOf(typeof(TrackableEntity)))
            {
                query = query.Where(e => !(e as TrackableEntity)!.IsDeleted);
            }

            return query;
        }
    }
}
