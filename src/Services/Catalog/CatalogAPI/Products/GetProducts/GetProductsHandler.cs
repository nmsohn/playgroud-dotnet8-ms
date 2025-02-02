namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResponse>;
public record GetProductsResponse(IEnumerable<Product> Products);

// TODO: Separate logging 
internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", request);
        // pagination required
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResponse(products);
    }
}