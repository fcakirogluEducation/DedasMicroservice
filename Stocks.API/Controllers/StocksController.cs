using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stocks.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { Id = id, Count = 20 });
        }
    }
}