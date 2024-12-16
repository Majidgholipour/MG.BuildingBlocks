using Autofac;
using MediatR;
using MG.BuildingBlock.Application.Attributes;
using MG.BuildingBlock.Application.Bus;
using MG.BuildingBlock.Domain.Models;
using MG.BuildingBlock.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MG.BuildingBlock.Infra.CrossCutting.Bus.Bus;

[Bean]
public class DomainEventsDispatcher<TContext> : IDomainEventsDispatcher<TContext> where TContext : DbContext
{
    private readonly IMediator _mediator;
    // private readonly ILifetimeScope _scope;
    // private readonly TContext _context;

    public DomainEventsDispatcher(IMediator mediator)
    {
        this._mediator = mediator;
        // this._scope = scope;
        // this._context = ordersContext;
    }

    public async Task DispatchEventsAsync(TContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        // var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
        // foreach (var domainEvent in domainEvents)
        // {
        //     Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
        //     var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
        //     var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
        //     {
        //         new NamedParameter("domainEvent", domainEvent)
        //     });
        //
        //     if (domainNotification != null)
        //     {
        //         domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
        //     }
        // }

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => { await _mediator.Publish(domainEvent); });

        // await Task.WhenAll(tasks);
        //
        // foreach (var domainEventNotification in domainEventNotifications)
        // {
        //     string type = domainEventNotification.GetType().FullName;
        //     var data = JsonConvert.SerializeObject(domainEventNotification);
        //     OutboxMessage outboxMessage = new OutboxMessage(
        //         domainEventNotification.DomainEvent.OccurredOn,
        //         type,
        //         data);
        //     this._ordersContext.OutboxMessages.Add(outboxMessage);
        // }
    }
}