using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportMaster.Models; // Убедитесь, что у вас есть доступ к модели Order и контексту базы данных

namespace SportMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly A1waezzkyContext _context; // Замените на ваш контекст базы данных

        public OrderController(A1waezzkyContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Customer) 
                .Include(o => o.OrderDetails) 
                .ToList();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Customer) // Включаем связанного клиента
                .Include(o => o.OrderDetails) // Включаем детали заказа
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(); // Возвращаем 404, если заказ не найден
            }
            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest(); // Возвращаем 400, если данные не предоставлены
            }

            // Устанавливаем дату заказа (если она не указана)
            order.OrderDate = DateTime.UtcNow;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || order.OrderId != id)
            {
                return BadRequest(); // Возвращаем 400, если данные некорректны
            }

            var existingOrder = _context.Orders.Find(id);
            if (existingOrder == null)
            {
                return NotFound(); // Возвращаем 404, если заказ не найден
            }

            // Обновляем данные заказа
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;

            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если обновление прошло успешно
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound(); // Возвращаем 404, если заказ не найден
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return NoContent(); // Возвращаем 204, если удаление прошло успешно
        }
    }
}