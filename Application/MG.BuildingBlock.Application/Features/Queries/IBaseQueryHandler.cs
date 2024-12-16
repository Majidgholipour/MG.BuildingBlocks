using MediatR;

namespace MG.BuildingBlock.Application.Features.Queries
{
    public interface IBaseQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : BaseQuery<TResult>
    {
    }
}