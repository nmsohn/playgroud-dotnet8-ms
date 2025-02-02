namespace CatalogAPI.Products.GetProducts;

// public record GetProductsRequest(int Page, int PageSize);

public class GetProductsEndpoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetProductsQuery(), cancellationToken);
            var result = response.Adapt<GetProductsResponse>();
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products");
    }
}