using MG.BuildingBlock.Domain.Enums;

namespace MG.BuildingBlock.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class , AllowMultiple = false)]
   public class ScopeAttribute : Attribute
    {
        private readonly ServiceLifetime _serviceLifetime;

        public ScopeAttribute(ServiceLifetime serviceLifetime)
        {
            _serviceLifetime = serviceLifetime;
        }


        public ServiceLifetime GetServiceLifetime()
        {
            return _serviceLifetime;
        }
    }
}
