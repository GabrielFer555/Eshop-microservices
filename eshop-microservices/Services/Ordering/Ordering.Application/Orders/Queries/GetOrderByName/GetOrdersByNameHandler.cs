namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orderName = OrderName.Of(query.OrderName);
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking() // good practice to optize queries
                .Where(x => x.OrderName.Value.Contains(query.OrderName))
                .OrderBy(x => x.OrderName.Value)    
                .ToListAsync(cancellationToken);


            return new GetOrderByNameResult(orders.ToOrderDtoList());
        }
    }
}
