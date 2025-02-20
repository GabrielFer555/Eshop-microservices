
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByNameRequest(string Name);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{name}", async ([FromRoute] string name, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(name));
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            }).WithName("GetOrdersByName")
                .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders by Name")
                .WithDescription("Get Orders by Name");
        }
    }
}
