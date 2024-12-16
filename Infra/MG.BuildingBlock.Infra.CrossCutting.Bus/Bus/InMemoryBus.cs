using MediatR;
using MG.BuildingBlock.Application.Attributes;
using MG.BuildingBlock.Application.Bus;
using MG.BuildingBlock.Application.Features.Queries;
using MG.BuildingBlock.Domain.Events;

namespace MG.BuildingBlock.Infra.CrossCutting.Bus.Bus;

[Bean]
public class InMemoryBus : IInMemoryBus
{
    private readonly IMediator _mediator;

    public InMemoryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TCommandResult> SendCommand<TCommandResult>(IRequest<TCommandResult> cmd)
    {
        return _mediator.Send(cmd);
    }

    public Task<TQueryResult> SendQuery<TQueryResult>(IRequest<TQueryResult> query)
    {
        return _mediator.Send(query);
    }

    public Task PublishEvent(DomainEvent @event)
    {
        return _mediator.Publish(@event);
    }
}