using PhoneShopeSharedLibrary.Models;
using PhoneShopeSharedLibrary.Responses;

namespace PhoneShopeSharedLibrary.Contracts;

public interface IProduct
{
    Task<ServiceResponse> AddProduct(Product product);
    Task<List<Product>> GetAllProducts(bool featuredProducts);
}
