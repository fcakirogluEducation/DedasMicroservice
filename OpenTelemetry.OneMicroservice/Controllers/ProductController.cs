using System.Diagnostics;
using Bus.Shared;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.OneMicroservice.Services;

namespace OpenTelemetry.OneMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(StockService stockService, IPublishEndpoint publishEndpoint) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = new AService();
            a.Calculate(2, 5);

            Activity.Current?.SetTag("main activity", "value");


            Activity.Current?.SetBaggage("user.name", "ahmet16");


            var stockResponse = await stockService.GetStock();
            ;
            return Ok($"Get all products, stock response : {stockResponse}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var a = new AService();
            a.Calculate(2, 5);

            Activity.Current?.SetTag("main activity", "value");


            Activity.Current?.SetBaggage("user.name", "ahmet16");


            await publishEndpoint.Publish(new OrderCreatedEvent("abc", DateTime.Now, 123, 200m, null));

            return Ok($"Get product by id {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Create product");
        }
    }
}