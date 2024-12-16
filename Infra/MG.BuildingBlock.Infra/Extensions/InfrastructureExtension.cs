using Garnet.Detail.Pagination.ListExtensions.DependencyInjection;
using MG.BuildingBlock.Infra.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.Extensions;

public static class InfrastructureExtension
{
    public static void AddMGInfrastructure(this IServiceCollection services,Action<IInfraRegistrationConfigurator> config)
    {
        var infrastructureConfiguration = new InfraRegistrationConfigurator(services);

        config(infrastructureConfiguration);

      
    }
    
    public static void UseMGInfrastructure(this IApplicationBuilder @this)
    {
        @this.UseGarnetPaginationListExtensions();
    }
}