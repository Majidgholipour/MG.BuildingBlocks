using AutoMapper;
using AutoMapper.QueryableExtensions;
using MG.BuildingBlock.Application.Services;
using Microsoft.Extensions.Logging;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapper.Services;

public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;
    private readonly ILogger<AutoMapperAdapter> _logger;

    public AutoMapperAdapter(IMapper mapper, ILogger<AutoMapperAdapter> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _logger.LogInformation("AutoMapper Adapter Start working");
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }

    public IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> sourceQueryable)
    {
        return sourceQueryable.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
    }
}
