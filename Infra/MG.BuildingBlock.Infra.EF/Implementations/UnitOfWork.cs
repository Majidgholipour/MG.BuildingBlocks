using MG.BuildingBlock.Application.Attributes;
using MG.BuildingBlock.Domain.Interfaces;
using MG.BuildingBlock.Infra.EF.Context;

namespace MG.BuildingBlock.Infra.EF.Implementations
{
    // public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    // {
    //     private readonly TContext _context;
    //     private readonly IDomainEventsDispatcher<TContext> _domainEventsDispatcher;
    //
    //     public UnitOfWork(
    //         TContext ordersContext,
    //         IDomainEventsDispatcher<TContext> domainEventsDispatcher)
    //     {
    //         this._context = ordersContext;
    //         this._domainEventsDispatcher = domainEventsDispatcher;
    //     }
    //
    //     public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    //     {
    //         // this._domainEventsDispatcher.DispatchEventsAsync(_context);
    //         return await this._context.SaveChangesAsync(cancellationToken) > 0;
    //     }
    // }
    
    [Bean]
    public class UnitOfWork(BaseContext dbContext) : IUnitOfWork
    {
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }

}