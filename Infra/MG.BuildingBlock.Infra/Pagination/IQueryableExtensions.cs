using AutoMapper;
using AutoMapper.QueryableExtensions;
using Garnet.Detail.Pagination.ListExtensions.Extensions;
using Garnet.Standard.Pagination;

namespace MG.BuildingBlock.Infra.Pagination
{
    public static class QueryableExtensions
    {
        public static MapperConfiguration _mapperConfiguration;
        static QueryableExtensions()
        {
        }
        public static IQueryable<T> ToDestination<T>(this IQueryable queryable)
        {
            return queryable.ProjectTo<T>(_mapperConfiguration);
        }

        public static IPagedElements<TEntity> UsePageable<TEntity>(this IQueryable<TEntity> receiver, IPagination pagination)
            where TEntity : class
        {
            return receiver.ToPagedResult(pagination);
        }

        public static Task<IPagedElements<TEntity>> UsePageableAsync<TEntity>(this IQueryable<TEntity> receiver, IPagination pagination)
          where TEntity : class
        {
            return receiver.ToPagedResultAsync(pagination);
        }

        public static Task<IPagedElements<TDestination>> UsePageableUseMapperAsync<TEntity, TDestination>(this IQueryable<TEntity> receiver, IPagination pagination)
         where TEntity : class
         where TDestination : class
        {
            var receiverDestination = receiver.ProjectTo<TDestination>(_mapperConfiguration);

            return receiverDestination.ToPagedResultAsync(pagination);
        }
    }
}
