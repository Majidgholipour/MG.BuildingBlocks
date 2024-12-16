using MG.BuildingBlock.Infra.Config;
using MG.BuildingBlock.Infra.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.EF.Extensions;

public static class EfExtension
{
    public static void AddDatabaseContext<TContext>(this IInfraRegistrationConfigurator infraConfig,
        Action<DbContextOptionsBuilder>? optionsAction = null)
        where TContext : BaseContext
    {
        infraConfig.AddDbContext<TContext>(options => { optionsAction?.Invoke(options); });

        // infraConfig.AddScoped<IDatabase, Database<TContext>>();

        infraConfig.AddScoped<BaseContext>(c => c.GetService<TContext>()!);

        // AddGenericQueries(infraConfig, typeof(AggregateQueryHandler<,>));
        // AddGenericQueries(infraConfig, typeof(AggregateQueryHandler<,,>));
        // AddGenericQueries(infraConfig, typeof(AnyQueryHandler<,>));
        // AddGenericQueries(infraConfig, typeof(AllQueryHandler<,,>));
        // AddGenericQueries(infraConfig, typeof(AllQueryHandler<,>));
        // AddGenericQueries(infraConfig, typeof(SingleQueryHandler<,>));
        // AddGenericQueries(infraConfig, typeof(SingleQueryHandler<,,>));
        // AddGenericQueries(infraConfig, typeof(PageableQueryHandler<,,>));

    }




    private static void AddGenericQueries(IInfraRegistrationConfigurator infraConfig, Type type)
    {
        var interfacesTypes = type.GetInterfaces().ToList();
        if (interfacesTypes.Count == 0)
        {
            infraConfig.AddScoped(type);
        }
        else
        {
            interfacesTypes.ForEach(x => infraConfig.AddScoped(GetServiceType(x, type), type));
        }
    }

    private static Type GetServiceType(Type serviceType, Type implementationType)
    {
        return implementationType.IsGenericType && !serviceType.IsGenericTypeDefinition
            ? serviceType.GetGenericTypeDefinition()
            : serviceType;
    }
}