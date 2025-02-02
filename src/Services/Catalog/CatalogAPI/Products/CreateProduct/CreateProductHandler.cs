namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, decimal Price, string ImageUrl) :ICommand<CreateProductResponse>;

public record CreateProductResponse(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // create entity
        // save
        // return response
        var product = new Product
            {
                Name = request.Name,
                Categories = request.Categories,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };
        
        session.Store(product);
        //transation with marten?
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResponse(product.Id);
    }
}