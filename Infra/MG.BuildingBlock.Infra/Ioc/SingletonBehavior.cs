using MG.BuildingBlock.Application.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ServiceLifetime = MG.BuildingBlock.Domain.Enums.ServiceLifetime;

namespace MG.BuildingBlock.Infra.Ioc
{
    [Scope(ServiceLifetime.Singleton)]
    internal class SingletonBehavior : IServiceBehavior
    {
        private IServiceCollection _serviceDescriptors;
        public SingletonBehavior(IServiceCollection serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }
 

        IServiceCollection IServiceBehavior.Add(Type serviceType, Type implementationType)
        {
            return _serviceDescriptors.AddSingleton(serviceType, implementationType);
        }

        IServiceCollection IServiceBehavior.Add(Type serviceType)
        {
            return _serviceDescriptors.AddSingleton(serviceType);
        }
    }
}
