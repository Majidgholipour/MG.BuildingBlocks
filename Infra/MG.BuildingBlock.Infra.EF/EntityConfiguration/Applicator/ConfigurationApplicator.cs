using MG.BuildingBlock.Infra.EF.EntityConfiguration.BaseModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MG.BuildingBlock.Infra.EF.EntityConfiguration.Applicator
{
    public static class ConfigurationApplicator
    {
        /// <summary>
        /// This extension create an instance of any class that inherites from <see cref="BaseModelConfiguration{TEntity, TPrimaryKey}"/>
        /// with an EntityTypeBuilder of <see cref="BaseModelConfiguration{TEntity, TPrimaryKey}"/> generic type.
        /// </summary>
        /// <param name="modelBuilder">modelBuilder.</param>
        /// <param name="type">type.</param>
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder, Type type)
        {
            var configurations = GetConfigurationWrappers(type);

            foreach (var config in configurations)
            {
                var entityTypeBuilder = GetEntityTypeBuilder(modelBuilder, config.Model);

                Activator.CreateInstance(config.Config, entityTypeBuilder);
            }
        }

        private static IList<ConfigurationWrapper> GetConfigurationWrappers(Type type)
        {
            var configTypes = GetAllConfigTypes(type);


            return configTypes.Select(BuildConfigurationWrapper).ToList();
        }

        private static IEnumerable<Type> GetAllConfigTypes(Type type)
        {
            var types = type.Assembly
                ?.GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    !type.IsGenericType &&
                    InheritsOrImplements(type, typeof(BaseModelConfiguration<,>)))
                .ToList();

            return types!;
        }

        private static ConfigurationWrapper BuildConfigurationWrapper(Type configurationType)
        {
            var model = configurationType.BaseType!.GetGenericArguments()[0];

            var config = new ConfigurationWrapper()
            {
                Model = model,
                Config = configurationType
            };

            return config;
        }

        private static EntityTypeBuilder GetEntityTypeBuilder(ModelBuilder modelBuilder, Type modelType)
        {
            var entityMethod = typeof(ModelBuilder)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "Entity" &&
                                     m.IsGenericMethod &&
                                     !m.GetParameters().Any());

            if (entityMethod == null)
            {
                throw new InvalidOperationException("Method Entity not found");
            }

            entityMethod = entityMethod.MakeGenericMethod(modelType);

            var entityTypeBuilder = (EntityTypeBuilder)entityMethod.Invoke(modelBuilder, null)!;

            return entityTypeBuilder;
        }

        #region InheritsOrImplements

        private static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                                   ? child.GetGenericTypeDefinition()
                                   : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                {
                    return true;
                }

                currentChild = currentChild.BaseType is { IsGenericType: true }
                                   ? currentChild.BaseType.GetGenericTypeDefinition()
                                   : currentChild.BaseType;

                if (currentChild == null)
                {
                    return false;
                }
            }
            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            bool shouldUseGenericType = !(parent.IsGenericType && parent.GetGenericTypeDefinition() != parent);

            if (parent.IsGenericType && shouldUseGenericType)
            {
                parent = parent.GetGenericTypeDefinition();
            }

            return parent;
        }

        #endregion
    }
}
