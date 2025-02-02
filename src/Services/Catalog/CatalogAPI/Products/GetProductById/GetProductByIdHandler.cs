namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;
//null check
public record GetProductByIdResponse(Product? Product);

internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        return product == null ? new GetProductByIdResponse(null) : new GetProductByIdResponse(product);
    }
}