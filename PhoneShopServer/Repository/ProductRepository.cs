using Microsoft.EntityFrameworkCore;
using PhoneShopeSharedLibrary.Contracts;
using PhoneShopeSharedLibrary.Models;
using PhoneShopeSharedLibrary.Responses;
using PhoneShopServer.Data;

namespace PhoneShopServer;

public class ProductRepository : IProduct
{
    private readonly AppDbContext _appDbContext;
    public ProductRepository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;   
    }
    public async Task<ServiceResponse> AddProduct(Product product)
    {
        if(product is null) return new ServiceResponse(false, "Failed");
        var (flag, message) = await CheckName(product.Name!);
        if (flag) {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
            return new ServiceResponse(true, "Product added successfully");
        }
        return new ServiceResponse(false, message);
    }

    public async Task<List<Product>> GetAllProducts(bool featuredProducts)
    {
        if (featuredProducts) return await _appDbContext.Products.Where(x => x.Featured).ToListAsync();
        return await _appDbContext.Products.ToListAsync();
    }

    private async Task<ServiceResponse> CheckName(string name) {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
        
        if(product != null) return new ServiceResponse(false, "Product already exists");
        
        return new ServiceResponse(true, null!);
    }
}
