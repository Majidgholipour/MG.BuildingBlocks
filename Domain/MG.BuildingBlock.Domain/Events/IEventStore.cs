using MG.BuildingBlock.Domain.SeedWork;

namespace MG.BuildingBlock.Domain.Events;

public interface IEventStore<TResult>
{
    void Save<T>(T theEvent)
        where T : DomainEvent;
}
