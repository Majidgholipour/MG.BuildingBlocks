using System.Reflection;
using MG.BuildingBlock.Application.Services;
using MG.BuildingBlock.Infra.Mappers.AutoMapper.Options;
using MG.BuildingBlock.Infra.Mappers.AutoMapper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapper.Extensions.DependencyInjection;
public static class AutoMapperServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkAutoMapperProfiles(this IServiceCollection services,
                                                          IConfiguration configuration,
                                                          string sectionName)
        => services.AddFrameworkAutoMapperProfiles(configuration.GetSection(sectionName));

    public static IServiceCollection AddFrameworkAutoMapperProfiles(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.Get<AutoMapperOption>();

        var assemblies = GetAssemblies(option?.AssmblyNamesForLoadProfiles!);

        return AddAutoMapperProfileConfig(services, assemblies);
    }

    public static IServiceCollection AddFrameworkAutoMapperProfiles(this IServiceCollection services, Action<AutoMapperOption> setupAction)
    {
        var option = new AutoMapperOption();
        setupAction.Invoke(option);

        var assemblies = GetAssemblies(option.AssmblyNamesForLoadProfiles);

        return AddAutoMapperProfileConfig(services, assemblies);

    }

    public static IServiceCollection AddFrameworkAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
    {
        return AddAutoMapperProfileConfig(services, [assembly]);
    }

    private static IServiceCollection AddAutoMapperProfileConfig(IServiceCollection services, List<Assembly> assemblies)
    {
        return services.AddAutoMapper(p => p.AddProfile(new MappingProfile(assemblies)))
                    .AddSingleton<IMapperAdapter, AutoMapperAdapter>();
    }

    private static List<Assembly> GetAssemblies(string assmblyNames)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default?.RuntimeLibraries;
        if (dependencies is null)
        {
            return assemblies;
        }

        foreach (var library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assmblyNames.Split(',')))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }

        return assemblies;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
        => assmblyName.Any(d => compilationLibrary.Name.Contains(d))
           || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
}
