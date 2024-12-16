using MediatR;

namespace MG.BuildingBlock.Application.Features.Commands;

public interface ICommandQuery<TResult> : IRequest<Result<TResult>>, IBaseRequest
{
}

public interface ICommandQuery : IRequest<Result>, IBaseRequest
{
}
