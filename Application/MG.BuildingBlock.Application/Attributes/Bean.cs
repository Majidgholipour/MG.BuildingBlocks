namespace MG.BuildingBlock.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
  public  class BeanAttribute : Attribute
    {
        public BeanAttribute()
        {

        }
    }
}
