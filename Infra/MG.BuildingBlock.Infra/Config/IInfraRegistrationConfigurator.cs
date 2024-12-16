using MassTransit;
using MG.BuildingBlock.Application.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.Config;

public interface IInfraRegistrationConfigurator : IServiceCollection
{
    void AddBehaviors();
    // void AddBus(Type handlerAssemblyMarkerType);
    // void AddIntegrationBus(Type consumerAssembly, IConfiguration configuration, Action<IBusRegistrationConfigurator> configure);
    void AddDomainServices();
    void AddMapper(Type mappingProfileAssemblyMarkerType);
    void AddApplication(Action<ApplicationConfig> applicationConfig);
    void AddWebModule();
    void AddMapsterMapper();
    void AddSwagger(IConfiguration configuration);
}