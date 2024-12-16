using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Domain.SeedWork;
using MG.BuildingBlock.Domain.SpecificationConfig;

namespace MG.BuildingBlock.Domain.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<(int TotalCount, IReadOnlyList<TEntity> Data)> ListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
}

