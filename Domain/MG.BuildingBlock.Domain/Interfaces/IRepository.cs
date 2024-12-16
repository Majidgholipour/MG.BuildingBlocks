using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Domain.SeedWork;

namespace MG.BuildingBlock.Domain.Interfaces;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
