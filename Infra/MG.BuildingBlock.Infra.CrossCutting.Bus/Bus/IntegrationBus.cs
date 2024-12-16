using MassTransit;
using MG.BuildingBlock.Application.Bus;

namespace MG.BuildingBlock.Infra.CrossCutting.Bus.Bus
{
    internal class IntegrationBus : IIntegrationBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public IntegrationBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : class
        {
            return _publishEndpoint.Publish(@event);
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : class
        {
            var uri = QueueNames.GetMessageUri(typeof(TCommand).Name);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(
                uri);

            await endpoint.Send(command);
        }
    }
}
