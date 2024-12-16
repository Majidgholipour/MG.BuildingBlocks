using MG.BuildingBlock.Domain.Interfaces;
using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Infra.EF.Context;

namespace MG.BuildingBlock.Infra.EF.Implementations;


public class Repository<TEntity>(
    BaseContext dbContext,
    ICurrentUser currentUser
    ) : ReadOnlyRepository<TEntity>(dbContext),
    IRepository<TEntity> where TEntity : Entity
{
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Created(currentUser.UserName);
        }
        
        await Set.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Updated(currentUser.UserName);
        }

        await Task.Run(() =>
        {
            Set.Update(entity);
        }, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Deleted(currentUser.UserName);

            await Task.Run(() =>
            {
                Set.Update(entity);
            }, cancellationToken);
        }
        else
        {
            await Task.Run(() =>
            {
                Set.Remove(entity);
            }, cancellationToken);
        }
    }
}