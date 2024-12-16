namespace MG.BuildingBlock.Domain.Events;

public class StoredDomainEvent : DomainEvent
{
    public StoredDomainEvent(DomainEvent theDomainEvent, string data, string user)
    {
        Id = Guid.NewGuid();
        AggregateId = theDomainEvent.AggregateId;
        MessageType = theDomainEvent.MessageType;
        Data = data;
        User = user;
    }

    // EF Constructor
    protected StoredDomainEvent()
    {
    }

    public Guid Id { get; private set; }

    public string Data { get; private set; }

    public string User { get; private set; }
}
