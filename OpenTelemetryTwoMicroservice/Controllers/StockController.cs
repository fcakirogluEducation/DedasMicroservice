using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryTwoMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var request = Request.Headers;

            var userName = Activity.Current?.GetBaggageItem("user.name");
            Activity.Current?.SetTag("user.name", userName);
            return Ok("Get all stock");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Get stock by id {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Create stock");
        }
    }
}