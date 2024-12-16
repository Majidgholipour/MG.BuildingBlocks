using MediatR;
using MG.BuildingBlock.Domain.SeedWork;

namespace MG.BuildingBlock.Domain.Events;

public abstract class DomainEvent : Message, IDomainEvent
{
    public DomainEvent()
    {
        this.OccurredOn = DateTime.Now;
    }

    public DateTime OccurredOn { get; }
}