using Microsoft.AspNetCore.Mvc;
using SportMaster.Models; 

namespace SportMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly A1waezzkyContext _context; 

        public CategoryController(A1waezzkyContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound(); 
            }
            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(); 
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || category.CategoryId != id)
            {
                return BadRequest(); // Возвращаем 400, если данные некорректны
            }

            var existingCategory = _context.Categories.Find(id);
            if (existingCategory == null)
            {
                return NotFound(); 
            }

            existingCategory.CategoryName = category.CategoryName;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound(); // Возвращаем 404, если категория не найдена
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если удаление прошло успешно
        }
    }
}