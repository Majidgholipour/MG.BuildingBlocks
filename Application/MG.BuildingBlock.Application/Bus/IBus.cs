namespace MG.BuildingBlock.Application.Bus;
public interface IBus
{
    Task Publish<TEvent>(TEvent @event);
    Task Send<TCommand>(TCommand command);
}
