using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportMaster.Models; // Убедитесь, что у вас есть доступ к модели Product и контексту базы данных

namespace SportMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly A1waezzkyContext _context; // Замените на ваш контекст базы данных

        public ProductController(A1waezzkyContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products
                .Include(p => p.Category) // Включаем связанную категорию
                .ToList();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products
                .Include(p => p.Category) // Включаем связанную категорию
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(); // Возвращаем 404, если продукт не найден
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(); // Возвращаем 400, если данные не предоставлены
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (product == null || product.ProductId != id)
            {
                return BadRequest(); // Возвращаем 400, если данные некорректны
            }

            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound(); // Возвращаем 404, если продукт не найден
            }

            // Обновляем данные продукта
            existingProduct.Name = product.Name;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если обновление прошло успешно
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound(); // Возвращаем 404, если продукт не найден
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если удаление прошло успешно
        }
    }
}