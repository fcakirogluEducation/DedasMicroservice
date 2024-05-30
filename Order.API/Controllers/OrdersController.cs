using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(OrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await orderService.OrderCreate2();

            return Created();
        }
    }
}