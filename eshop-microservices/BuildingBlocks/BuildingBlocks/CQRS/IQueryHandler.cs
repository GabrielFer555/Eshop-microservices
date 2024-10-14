using MediatR;
namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>:IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse> // validate if TQuery inherits from the interface IQuery
        where TResponse : notnull
    {
    }
}
