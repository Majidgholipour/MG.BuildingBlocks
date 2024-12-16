using MG.BuildingBlock.Application.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ServiceLifetime = MG.BuildingBlock.Domain.Enums.ServiceLifetime;

namespace MG.BuildingBlock.Infra.Ioc
{
    [Scope(ServiceLifetime.Scoped)]
    internal class ScopedBehavior : IServiceBehavior
    {
        private IServiceCollection _services;
        public ScopedBehavior(IServiceCollection services)
        {
            _services = services;
        }
 
        IServiceCollection IServiceBehavior.Add(Type serviceType, Type implementationType)
        {
            return _services.AddScoped(serviceType, implementationType);

        }

        IServiceCollection IServiceBehavior.Add(Type serviceType)
        {
            return _services.AddScoped(serviceType);
        }
    }
}
