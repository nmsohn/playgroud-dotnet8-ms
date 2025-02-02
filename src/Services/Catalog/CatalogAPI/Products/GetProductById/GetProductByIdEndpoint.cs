namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
                async (GetProductByIdRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = request.Adapt<GetProductByIdQuery>();
                    var response = await sender.Send(new GetProductByIdQuery(query.Id), cancellationToken);
                    var result = response.Adapt<GetProductByIdResponse>();
                    return Results.Ok(result);
                })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get a product by id")
            .WithDescription("Get a product by id");
    }
}