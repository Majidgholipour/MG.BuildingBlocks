// using MG.BuildingBlock.Infra.EF.Context;
//
// namespace MG.BuildingBlock.Infra.EF.Data;
//
// public class Database<TContext> : IDatabase
//     where TContext : BaseContext
// {
//     private readonly TContext _context;
//     private readonly IEntityTracker _entityTracker;
//
//     public Database(TContext context, IEntityTracker entityTracker)
//     {
//         _context = context;
//         _entityTracker = entityTracker;
//     }
//
//     public Task Persist()
//     {
//         var entities = _entityTracker.GetEntities();
//         foreach (var entity in entities)
//         {
//             switch (entity.State)
//             {
//                 case EntityState.Added:
//                     Add(entity.Entity);
//                     break;
//                 case EntityState.Updated:
//                     break;
//                 case EntityState.Deleted:
//                     Remove(entity.Entity);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//         }
//
//         return _context.SaveChangesAsync();
//     }
//
//
//     private void Add(object entity)
//     {
//         _context.Add(entity);
//     }
//
//     private void Remove(object entity)
//     {
//         _context.Remove(entity);
//     }
// }