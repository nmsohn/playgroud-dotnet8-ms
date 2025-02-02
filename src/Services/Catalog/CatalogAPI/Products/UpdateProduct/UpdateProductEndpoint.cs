namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageUrl);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}",
                async (UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();
                    var result = await sender.Send(command, cancellationToken);
                    var response = result.Adapt<UpdateProductResponse>();
                    return Results.Ok(response);
                })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update a product")
            .WithDescription("Update a product");
    }
}