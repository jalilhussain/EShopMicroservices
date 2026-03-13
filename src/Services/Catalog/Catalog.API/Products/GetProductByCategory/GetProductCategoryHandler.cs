
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductCategoryQuery(string Category) : IRequest<GetProductCategoryResult>;
public record GetProductCategoryResult(IEnumerable<Product> Products);


public class GetProductCategoryQueryHandler
    (IDocumentSession session)
     : IRequestHandler<GetProductCategoryQuery, GetProductCategoryResult>
{
    public async Task<GetProductCategoryResult> Handle(GetProductCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync();
        return new GetProductCategoryResult(products);
        throw new NotImplementedException();
    }
}

