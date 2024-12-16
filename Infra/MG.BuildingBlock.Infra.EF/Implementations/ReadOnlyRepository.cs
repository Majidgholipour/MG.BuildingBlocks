using MG.BuildingBlock.Domain.Interfaces;
using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Domain.SpecificationConfig;
using MG.BuildingBlock.Infra.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace MG.BuildingBlock.Infra.EF.Implementations;

public class ReadOnlyRepository<TEntity>(
    BaseContext dbContext
    ) : RepositoryProperties<TEntity>(dbContext), IReadOnlyRepository<TEntity> where TEntity : Entity
{
    // public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    // {
    //     return await SetAsNoTracking.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    // }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var query = SetAsNoTracking.Specify(specification);

        var totalCount = 0;

        if (specification.IsPagingEnabled)
        {
            totalCount = await query.CountAsync(cancellationToken);
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        var data = await query.ToListAsync(cancellationToken);

        return (totalCount, data);
    }
}
