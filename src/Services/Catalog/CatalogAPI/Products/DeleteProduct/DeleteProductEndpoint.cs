namespace CatalogAPI.Products.DeleteProduct;

// public record DeleteProductRequest(Guid Id);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",
                async (Guid Id, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(Id, cancellationToken);
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);
                })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete a product")
            .WithDescription("Delete a product");
    }
}