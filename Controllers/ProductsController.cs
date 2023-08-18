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

    public ProductsController(IRepository<Product> repository)
    {
        _productsRepository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync()
    {
        return Ok((await _productsRepository.GetAllAsync()).Select(item => item.AsProductDTO()));
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(ProductDTO product)
    {
        try
        {
            Product newProduct = product.Type switch
            {
                "Факел" => new Torch(product.Name, product.Description, product.Price),
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