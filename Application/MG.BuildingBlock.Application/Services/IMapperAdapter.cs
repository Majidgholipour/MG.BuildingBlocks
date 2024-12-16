namespace MG.BuildingBlock.Application.Services;

public interface IMapperAdapter
{
    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> sourceQueryable);
}