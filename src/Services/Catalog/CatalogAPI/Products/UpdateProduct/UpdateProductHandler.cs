namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageUrl) : ICommand<UpdateProductResponse>;
public record UpdateProductResponse(string Name, List<string> Categories, string Description, decimal Price, string ImageUrl);

internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        // null check
        if (product == null)
        {
            return null;
        }
        
        product.Name = request.Name;
        product.Categories = request.Categories;
        product.Description = request.Description;
        product.Price = request.Price;
        product.ImageUrl = request.ImageUrl;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResponse(product.Name, product.Categories, product.Description, product.Price, product.ImageUrl);
    }
}