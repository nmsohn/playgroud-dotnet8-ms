namespace CatalogAPI.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Categories, string Description, decimal Price, string ImageUrl);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (CreateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var result = await sender.Send(command, cancellationToken);
                    var response = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{response.Id}", response);
                })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product");
    }
}