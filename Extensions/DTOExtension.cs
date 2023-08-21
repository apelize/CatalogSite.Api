using DTO;
using Entities;

namespace Extensions;

public static partial class Extensions
{
    public static ProductDTO AsProductDTO(this Product product)
    {
        return new ProductDTO(product._id.ToString(), product.Name!, product.Description!, product.Type!, product.Image!, product.Price);
    }
}