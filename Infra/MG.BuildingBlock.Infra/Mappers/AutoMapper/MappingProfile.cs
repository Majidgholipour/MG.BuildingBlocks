using System.Reflection;
using AutoMapper;
using MG.BuildingBlock.Infra.Mappers.AutoMapper.Contracts;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapper;
public class MappingProfile : Profile
{
    public MappingProfile(List<Assembly> assemblies)
    {
        ApplyMapFromMappingsFromAssembly(assemblies);
        ApplyMapToMappingsFromAssembly(assemblies);
    }

    public MappingProfile(Assembly assembly)
    {
        ApplyMapFromMappingsFromAssembly([assembly]);
        ApplyMapToMappingsFromAssembly([assembly]);
    }

    private void ApplyMapToMappingsFromAssembly(List<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            MakeMappingFor(assembly, typeof(IMapTo<>), nameof(IMapTo<object>.MapTo));
        }
    }

    private void ApplyMapFromMappingsFromAssembly(List<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            MakeMappingFor(assembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.MapFrom));
        }
    }

    private void MakeMappingFor(Assembly assembly, Type mapToType, string mappingMethodName)
    {
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapToType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, [this]);
                    }
                }
            }
        }
    }
}
