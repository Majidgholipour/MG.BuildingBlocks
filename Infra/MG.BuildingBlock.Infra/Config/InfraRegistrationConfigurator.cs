using System.Collections;
using MG.BuildingBlock.Application.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace MG.BuildingBlock.Infra.Config;

public class InfraRegistrationConfigurator : IInfraRegistrationConfigurator
{
    private readonly IServiceCollection _serviceCollection;

    public InfraRegistrationConfigurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public IEnumerator<ServiceDescriptor> GetEnumerator()
    {
        return _serviceCollection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(ServiceDescriptor item)
    {
        _serviceCollection.Add(item);
    }

    public void Clear()
    {
        _serviceCollection.Clear();
    }

    public bool Contains(ServiceDescriptor item)
    {
        return _serviceCollection.Contains(item);
    }

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
    {
        _serviceCollection.CopyTo(array, arrayIndex);
    }

    public bool Remove(ServiceDescriptor item)
    {
        return _serviceCollection.Remove(item);
    }

    public int Count { get; }
    public bool IsReadOnly { get; }

    public int IndexOf(ServiceDescriptor item)
    {
        return _serviceCollection.IndexOf(item);
    }

    public void Insert(int index, ServiceDescriptor item)
    {
        _serviceCollection.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _serviceCollection.RemoveAt(index);
    }

    public ServiceDescriptor this[int index]
    {
        get => _serviceCollection[index];
        set => _serviceCollection[index] = value;
    }

    public void AddBehaviors()
    {
    }

    // public void AddBus(Type handlerAssemblyMarkerType)
    // {
    //     _serviceCollection.AddMediatR(p => p.RegisterServicesFromAssemblyContaining(handlerAssemblyMarkerType));
    //     _serviceCollection.AddScoped<IInMemoryBus,  IInMemoryBus>();
    // }


    public void AddDomainServices()
    {
        // _serviceCollection.AddSingleton<IEntityTracker, EntityTracker>();
        // _serviceCollection.AddSingleton<IDomainEventTracker, DomainEventTracker>();
    }

    public void AddMapper(Type mappingProfileAssemblyMarkerType)
    {
        //line below should replace with AddFrameworkAutoMapperProfiles method, this change my also impact your profiles because
        //you have to use IMapTo and IMapFrom interfaces for your mappings instead of creating a profile for each mapping you wanna create.
        _serviceCollection.AddAutoMapper(mappingProfileAssemblyMarkerType.Assembly);
    }

    public void AddMapsterMapper()
    {
    }

    public void AddSwagger(IConfiguration configuration)
    {
        _serviceCollection.AddSwaggerModule(configuration);
    }

    public void AddApplication(Action<ApplicationConfig> applicationConfig)
    {
        var appConfig = new ApplicationConfig();
        applicationConfig(appConfig);
        _serviceCollection.AddSingleton(appConfig);
    }

    public void AddWebModule()
    {
        // _serviceCollection.AddGarnetPaginationAsp(new PaginationConfig() { StartPageNumber = StartPageNumber.Zero });
        //
        // _serviceCollection.AddControllers(options =>
        //     {
        //         options.ModelBinderProviders.Insert(0, new GarnetPaginationModelBinderProvider());
        //         //  AuditStartUp.AddAudit(options);
        //     })
        //     .AddNewtonsoftJson(op =>
        //     {
        //         op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        //         op.SerializerSettings.Converters.Add(
        //             new Newtonsoft.Json.Converters.StringEnumConverter(new SnakeUpperCaseNaming()));
        //     });
    }

    // public void AddIntegrationBus(Type consumerAssembly, IConfiguration configuration,
    //     Action<IBusRegistrationConfigurator> configure)
    // {
    //     var assembly = consumerAssembly.Assembly;
    //
    //     _serviceCollection.AddScoped<IIntegrationBus, IntegrationBus>();
    //     _serviceCollection.AddMassTransit(configure);
    //
    //     _serviceCollection.AddMassTransit(cfg =>
    //     {
    //         cfg.SetKebabCaseEndpointNameFormatter();
    //
    //         cfg.AddConsumers(assembly);
    //
    //         cfg.AddDelayedMessageScheduler();
    //         cfg.UsingRabbitMq((context, cfgg) =>
    //         {
    //             cfgg.UseDelayedMessageScheduler();
    //             cfgg.ConcurrentMessageLimit = 100;
    //             cfgg.ConfigureEndpoints(context);
    //             cfgg.Host(configuration.GetSection("RabbitConfig:host").Value, h =>
    //             {
    //                 h.Username(configuration.GetSection("RabbitConfig:username").Value!);
    //                 h.Password(configuration.GetSection("RabbitConfig:password").Value!);
    //             });
    //         });
    //
    //
    //         configure(cfg);
    //     });
    // }

    private class SnakeUpperCaseNaming : SnakeCaseNamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return base.ResolvePropertyName(name).ToUpper();
        }
    }
}