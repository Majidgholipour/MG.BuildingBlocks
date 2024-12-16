using MediatR;
using MG.BuildingBlock.Application.Features.Queries;

namespace MG.BuildingBlock.Application.Bus
{
    /// <summary>
    /// It is used as a mediator to send and handle requests inside a single service.
    /// </summary>
    public interface IInMemoryBus
    {
        Task<TCommandResult> SendCommand<TCommandResult>(IRequest<TCommandResult> cmd);
        Task<TQueryResult> SendQuery<TQueryResult>(IRequest<TQueryResult> query);
    }
}