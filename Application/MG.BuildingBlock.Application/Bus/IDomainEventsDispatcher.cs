namespace MG.BuildingBlock.Application.Bus
{
    public interface IDomainEventsDispatcher<in TContext>
    {
        Task DispatchEventsAsync(TContext context);
    }
}