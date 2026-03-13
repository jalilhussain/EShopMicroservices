
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductCategoryResponse(IEnumerable<Product> Products);


public class GetProductCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductCategoryQuery(category);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductCategoryResponse>();
            return Results.Ok(response);
        })
         .WithName("GetProductByCategory")
         .Produces<GetProductCategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithDescription("Retrieves a list of products that belong to a specific category.");
    }
}

