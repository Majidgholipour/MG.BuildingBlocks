namespace MG.BuildingBlock.Application.Bus;

public interface IIntegrationBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : class;
    Task Send<TCommand>(TCommand command) where TCommand : class;
}
