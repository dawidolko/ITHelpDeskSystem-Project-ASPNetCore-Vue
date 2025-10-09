using Microsoft.AspNetCore.Mvc;
using SortListApi.Models;

namespace SortListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // In-memory data store for demo purposes
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200.00m, Stock = 15, CreatedDate = DateTime.Now.AddDays(-30) },
        new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 25.50m, Stock = 50, CreatedDate = DateTime.Now.AddDays(-20) },
        new Product { Id = 3, Name = "Keyboard", Category = "Electronics", Price = 75.00m, Stock = 30, CreatedDate = DateTime.Now.AddDays(-15) },
        new Product { Id = 4, Name = "Monitor", Category = "Electronics", Price = 350.00m, Stock = 20, CreatedDate = DateTime.Now.AddDays(-25) },
        new Product { Id = 5, Name = "Desk Chair", Category = "Furniture", Price = 200.00m, Stock = 10, CreatedDate = DateTime.Now.AddDays(-10) },
        new Product { Id = 6, Name = "Desk", Category = "Furniture", Price = 400.00m, Stock = 5, CreatedDate = DateTime.Now.AddDays(-5) },
        new Product { Id = 7, Name = "Notebook", Category = "Stationery", Price = 5.00m, Stock = 100, CreatedDate = DateTime.Now.AddDays(-2) },
        new Product { Id = 8, Name = "Pen", Category = "Stationery", Price = 1.50m, Stock = 200, CreatedDate = DateTime.Now.AddDays(-1) },
        new Product { Id = 9, Name = "Headphones", Category = "Electronics", Price = 80.00m, Stock = 25, CreatedDate = DateTime.Now.AddDays(-12) },
        new Product { Id = 10, Name = "Webcam", Category = "Electronics", Price = 120.00m, Stock = 18, CreatedDate = DateTime.Now.AddDays(-8) }
    };

    // GET: api/products
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts(
        [FromQuery] string? sortBy = "id",
        [FromQuery] string? sortOrder = "asc",
        [FromQuery] string? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] string? search = null)
    {
        var query = Products.AsQueryable();

        // Filtering by category
        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        // Filtering by price range
        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        // Search by name
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        // Sorting
        query = sortBy?.ToLower() switch
        {
            "name" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.Name) 
                : query.OrderBy(p => p.Name),
            "category" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.Category) 
                : query.OrderBy(p => p.Category),
            "price" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.Price) 
                : query.OrderBy(p => p.Price),
            "stock" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.Stock) 
                : query.OrderBy(p => p.Stock),
            "createddate" => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.CreatedDate) 
                : query.OrderBy(p => p.CreatedDate),
            _ => sortOrder?.ToLower() == "desc" 
                ? query.OrderByDescending(p => p.Id) 
                : query.OrderBy(p => p.Id)
        };

        return Ok(query.ToList());
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        product.Id = Products.Max(p => p.Id) + 1;
        product.CreatedDate = DateTime.Now;
        Products.Add(product);
        
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = product.Name;
        existingProduct.Category = product.Category;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        
        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        Products.Remove(product);
        return NoContent();
    }

    // GET: api/products/categories
    [HttpGet("categories")]
    public ActionResult<IEnumerable<string>> GetCategories()
    {
        var categories = Products.Select(p => p.Category).Distinct().OrderBy(c => c);
        return Ok(categories);
    }
}
