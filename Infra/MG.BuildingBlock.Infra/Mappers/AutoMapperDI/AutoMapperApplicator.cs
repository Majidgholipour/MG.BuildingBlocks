using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.Mappers.AutoMapperDI
{
    public static class AutoMapperApplicator
    {


        public static IServiceCollection ApplyAllConfigurations(this IServiceCollection @this, Action<IMapperConfigurationExpression> customerProfileRegister = null)
        {
            var profiles = Assembly.Load("TelApp.Application")
              .GetTypes()
              .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
              .Where(t => !t.GetTypeInfo().IsAbstract)
              .ToList();

            // profiles.AddRange(
            //         Assembly.Load("Cheque.Api")
            //             .GetTypes()
            //             .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
            //             .Where(t => !t.GetTypeInfo().IsAbstract)
            //             .ToList()
            //     );
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                
                profiles.ForEach(c => mc.AddProfile(c));

                customerProfileRegister?.Invoke(mc);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            @this.AddSingleton(mapper);

            Pagination.QueryableExtensions._mapperConfiguration = mappingConfig;
            return @this;
        }
    }
}
