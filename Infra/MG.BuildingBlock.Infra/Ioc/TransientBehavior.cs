using MG.BuildingBlock.Application.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.Ioc
{
    [Scope(Domain.Enums.ServiceLifetime.Transient)]
    internal class TransientBehavior : IServiceBehavior
    {
        private IServiceCollection _services;
        public TransientBehavior(IServiceCollection services)
        {
            _services = services;
        }

        IServiceCollection IServiceBehavior.Add(Type serviceType, Type implementationType)
        {
            return _services.AddTransient(serviceType, implementationType);

        }

        IServiceCollection IServiceBehavior.Add(Type serviceType)
        {
            return _services.AddTransient(serviceType);
        }
    }
}
