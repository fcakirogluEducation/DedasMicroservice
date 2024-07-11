using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(string name)
        {
            return Ok(new { isSuccess = true, Id = 10, Name = name });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { isSuccess = true });
        }
    }
}