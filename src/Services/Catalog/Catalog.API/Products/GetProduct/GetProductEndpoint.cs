
namespace Catalog.API.Products.GetProduct;

public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var query = new GetProductQuery();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
         .WithName("GetProducts")
         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithDescription("Retrieves a list of all products in the catalog.");
    }
}

