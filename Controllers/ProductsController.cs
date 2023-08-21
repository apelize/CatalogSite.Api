using DTO;
using Entities;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _productsRepository;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(IRepository<Product> repository, ILogger<ProductsController> logger)
    {
        _productsRepository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync()
    {
        _logger.LogInformation("Get all request");
        return Ok((await _productsRepository.GetAllAsync()).Select(item => item.AsProductDTO()));
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(ProductDTO product)
    {
        _logger.LogInformation("Post request");
        try
        {
            Product newProduct = product.ProductType switch
            {
                "Факел" => new Torch(product.ProductName, product.ProductDescription, product.ProductImage, product.ProductPrice),
                _ => throw new ArgumentException("Неизвестный тип продукта")
            };
            await _productsRepository.CreateAsync(newProduct);
            return Ok(newProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductAsync(Guid id)
    {
        await _productsRepository.DeleteAsync(id);
        return NoContent();
    }
}