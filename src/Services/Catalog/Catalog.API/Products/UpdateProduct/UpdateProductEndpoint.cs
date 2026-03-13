
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string ImageFile, List<string> Category, decimal Price);

public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(request.Id, request.Name, request.ImageFile, request.Category, request.Price);
            var result = await sender.Send(command);
            // Manual mapping test
            var response = new UpdateProductResponse(result.IsSuccess);
            return Results.Ok(response);
        })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Updates an existing product.")
            .WithDescription("Updates an existing product with the provided details. Returns a success status if the update is successful.");
 
    }
}

