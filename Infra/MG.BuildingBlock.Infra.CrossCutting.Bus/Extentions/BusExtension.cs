using MassTransit;
using MG.BuildingBlock.Application.Bus;
using MG.BuildingBlock.Infra.Config;
using MG.BuildingBlock.Infra.CrossCutting.Bus.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.CrossCutting.Bus.Extentions;

public static class BusExtension
{
    public static void AddBusConfig(this IInfraRegistrationConfigurator infraConfig, Type handlerAssemblyMarkerType)
    {
        infraConfig.AddMediatR(p => p.RegisterServicesFromAssemblyContaining(handlerAssemblyMarkerType));
        infraConfig.AddScoped<IInMemoryBus, IInMemoryBus>();
        infraConfig.AddScoped(typeof(IDomainEventsDispatcher<>), typeof(DomainEventsDispatcher<>));
    }

    public static void AddIntegrationBus(this IInfraRegistrationConfigurator infraConfig, Type consumerAssembly,
        IConfiguration configuration, Action<IBusRegistrationConfigurator> configure)
    {
        var assembly = consumerAssembly.Assembly;

        infraConfig.AddScoped<IIntegrationBus, IntegrationBus>();
        infraConfig.AddMassTransit(configure);

        infraConfig.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddConsumers(assembly);

            cfg.AddDelayedMessageScheduler();
            cfg.UsingRabbitMq((context, cfgg) =>
            {
                cfgg.UseDelayedMessageScheduler();
                cfgg.ConcurrentMessageLimit = 100;
                cfgg.ConfigureEndpoints(context);
                cfgg.Host(configuration.GetSection("RabbitConfig:host").Value, h =>
                {
                    h.Username(configuration.GetSection("RabbitConfig:username").Value!);
                    h.Password(configuration.GetSection("RabbitConfig:password").Value!);
                });
            });
            configure(cfg);
        });
    }
}