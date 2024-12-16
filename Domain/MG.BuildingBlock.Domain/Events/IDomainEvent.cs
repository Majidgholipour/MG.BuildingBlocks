using MediatR;

namespace MG.BuildingBlock.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}