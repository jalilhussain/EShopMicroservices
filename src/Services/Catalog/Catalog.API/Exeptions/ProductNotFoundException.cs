using BuildingBlocks.Exceptions;

namespace Catalog.API.Exeptions;
public class ProductNotFoundException: NotFoundException
{

    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
    }
}

