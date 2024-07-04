using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.OneMicroservice.Services;

namespace OpenTelemetry.OneMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(StockService stockService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = new AService();
            a.Calculate(2, 5);

            Activity.Current?.SetTag("main activity", "value");


            Activity.Current?.SetBaggage("user.name", "ahmet16");


            var stockResponse = await stockService.GetStock();


            return Ok($"Get all products, stock response : {stockResponse}");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Get product by id {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Create product");
        }
    }
}