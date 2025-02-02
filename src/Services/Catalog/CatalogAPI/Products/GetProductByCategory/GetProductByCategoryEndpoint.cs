using System.Collections;

namespace CatalogAPI.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
                async (string category, ISender sender, CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(new GetProductByCategoryQuery(category), cancellationToken);
                    var result = response.Adapt<GetProductByCategoryResponse>();
                    return Results.Ok(result);
                })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
        
    }
}