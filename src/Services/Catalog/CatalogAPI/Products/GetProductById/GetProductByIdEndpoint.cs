namespace CatalogAPI.Products.GetProductById;

// public record GetProductByIdRequest(Guid Id);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
                async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
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