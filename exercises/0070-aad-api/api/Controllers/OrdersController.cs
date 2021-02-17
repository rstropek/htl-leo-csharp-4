using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Collections.Concurrent;
using System.Net;

namespace ProtectedApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public record Order(int ID, string Customer, decimal Revenue);

        public readonly static ConcurrentBag<Order> orders = new()
        {
            new(1, "Foo", 42m),
            new(2, "Bar", 84m)
        };

        [HttpGet]
        [RequiredScope("read")]
        [Authorize(Policy = "RainerOnly")]
        public IActionResult GetAllOrders()
        {
            return Ok(orders);
        }

        [HttpPost]
        [RequiredScope("write")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            orders.Add(order);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("clear")]
        [RequiredScope("admin")]
        public IActionResult ClearOrders()
        {
            orders.Clear();
            return NoContent();
        }
    }
}
