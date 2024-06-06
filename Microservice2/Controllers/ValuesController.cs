using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCityList()
        {
            List<string> cityList = ["Ankara", "İstanbul"];

            return Ok(cityList);
        }
    }
}