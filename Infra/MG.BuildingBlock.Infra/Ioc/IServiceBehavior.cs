using Microsoft.Extensions.DependencyInjection;

namespace MG.BuildingBlock.Infra.Ioc
{
    internal  interface IServiceBehavior
    {

   
       IServiceCollection  Add( Type serviceType,Type implementationType);

       IServiceCollection Add(Type serviceType);
    }
}
