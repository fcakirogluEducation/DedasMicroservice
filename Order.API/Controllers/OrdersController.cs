using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(OrderService orderService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create()
        {
            orderService.OrderCrete();

            return Created();
        }
    }
}