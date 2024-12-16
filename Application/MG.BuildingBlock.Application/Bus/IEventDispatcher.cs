using MG.BuildingBlock.Domain.Events;

namespace MG.BuildingBlock.Application.Bus;

public interface IEventDispatcher
{
    Task DispatchAsync(DomainEvent @event, CancellationToken cancellationToken = default);
}