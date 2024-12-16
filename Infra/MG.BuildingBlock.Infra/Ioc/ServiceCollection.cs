using System.Reflection;
using MG.BuildingBlock.Application.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ServiceLifetime = MG.BuildingBlock.Domain.Enums.ServiceLifetime;

namespace MG.BuildingBlock.Infra.Ioc
{
    public class ServiceCollection
    {


        private IServiceCollection _serviceDescriptors;


 
        public ServiceCollection(IServiceCollection serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

 
        public void AddServiceDescriptors()
        {

            var projects = new List<string>()
             {"MG.BuildingBlock.Infra.EF","MG.BuildingBlock.Infra.CrossCutting.Bus","TelApp.Infra", "TelApp.Infra", "TelApp.Application", "TelApp.Domain" };
            var typesToRegister = new List<Type>();
            foreach (var item in projects)
            {
                typesToRegister.AddRange(GetTypesByAssemblyName(item));
            }
          
                
            foreach (var item in typesToRegister)
            {
                var service = GetServiceBehavior(GetServiceLifetime(item));
                var InterfacesTypes = item.GetInterfaces().ToList();
                if (InterfacesTypes.Count == 0)
                    service.Add(item);
                else
                    InterfacesTypes.ForEach(x => service.Add(GetServiceType(x, item), item));
            }
        }
        private Type GetServiceType(Type serviceType, Type implementationType)
        {
            return implementationType.IsGenericType && !serviceType.IsGenericTypeDefinition
                ? serviceType.GetGenericTypeDefinition()
                : serviceType;
        }
        private List<Type> GetTypesByAssemblyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<Type>();
            
            return Assembly.Load(name)
               .GetTypes()
               .Where(x => x.GetCustomAttributes(typeof(BeanAttribute), false).Length > 0)
               .ToList();
        }
        private ServiceLifetime GetServiceLifetime(Type type)
        {
            if (type.GetCustomAttributes(typeof(ScopeAttribute), false).Length > 0)
                return ((ScopeAttribute)type.GetCustomAttributes(typeof(ScopeAttribute), false)[0]).GetServiceLifetime();
            else
                return ServiceLifetime.Scoped;
        }

 

        private IServiceBehavior GetServiceBehavior(ServiceLifetime serviceLifetime)
        {
            
            var type = Assembly.Load("MG.BuildingBlock.Infra")
               .GetTypes() 
                .FirstOrDefault(x =>
                 x.IsClass &&
                  x.GetInterfaces().Any(i => i.Name == "IServiceBehavior") &&
                  x.GetCustomAttribute<ScopeAttribute>().GetServiceLifetime() == serviceLifetime
                );
            return (IServiceBehavior)Activator.CreateInstance(type, new Object[] { _serviceDescriptors });
        }

    }
}
