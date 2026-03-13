using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        // Open a lightweight session to interact with the Postgres database
        using var session = store.LightweightSession();

        // Check if there are already products in the database. If yes, stop seeding.
        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        // If the database is empty, insert the preconfigured products
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product()
        {
            Id = new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
            Name = "Huawei Plus",
            Description = "This phone is the company's biggest change to its flagship.",
            ImageFile = "product-3.png",
            Price = 650.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
            Name = "Xiaomi Mi 9",
            Description = "A highly anticipated smartphone.",
            ImageFile = "product-4.png",
            Price = 470.00M,
            Category = new List<string> { "Smart Phone" }
        }
    };
}