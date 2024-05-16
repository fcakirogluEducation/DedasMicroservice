using System.Reflection.Metadata.Ecma335;
using Clean.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        //get product
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(productService.GetProduct());
        }
    }
}