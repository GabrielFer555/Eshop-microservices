

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest request):IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginationResult<OrderDto> PaginationResult);
}
