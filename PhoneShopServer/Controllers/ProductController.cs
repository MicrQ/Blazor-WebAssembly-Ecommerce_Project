using Microsoft.AspNetCore.Mvc;
using PhoneShopeSharedLibrary.Contracts;
using PhoneShopeSharedLibrary.Models;
using PhoneShopeSharedLibrary.Responses;

namespace PhoneShopServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProduct _productService;
    public ProductController(IProduct productService)
    {
        this._productService = productService;
    }

    [HttpGet]
    public  async Task<ActionResult<List<Product>>> GetAllProducts(bool featuredProducts)
    {
        var products = await _productService.GetAllProducts(featuredProducts);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> AddProduct(Product product)
    {
        if (product is null) return BadRequest("Model is Null");
        var response = await _productService.AddProduct(product);

        return Ok(response);
    }
}
