using Microsoft.AspNetCore.Mvc;
using SportMaster.Models; 

namespace SportMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly A1waezzkyContext _context; // Замените на ваш контекст базы данных

        public CustomerController(A1waezzkyContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound(); // Возвращаем 404, если клиент не найден
            }
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(); // Возвращаем 400, если данные не предоставлены
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.CustomerId != id)
            {
                return BadRequest(); // Возвращаем 400, если данные некорректны
            }

            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound(); // Возвращаем 404, если клиент не найден
            }

            // Обновляем данные клиента
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;

            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если обновление прошло успешно
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound(); // Возвращаем 404, если клиент не найден
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если удаление прошло успешно
        }
    }
}