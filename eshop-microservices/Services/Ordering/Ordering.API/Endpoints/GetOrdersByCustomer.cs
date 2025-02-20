
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByCustomerRequest(Guid Id);
    public record GetOrdersByCustomersResponse(List<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customers/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(id));
                var response = result.Adapt<GetOrdersByCustomersResponse>();

                return Results.Ok(response);
            }).WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomersResponse>(StatusCodes.Status200OK)
                .WithSummary("Get Orders By Customer")
                .WithDescription("Get Orders By Customer");
        }
    }
}
