
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetProductByIdQueryHandler(IDocumentSession session)
    : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        return new GetProductByIdResult(product);
    }
}

