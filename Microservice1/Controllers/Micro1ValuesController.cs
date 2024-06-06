using Microservice1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Micro1ValuesController(
        MicroService2Service microService2Service,
        IMicroService2Refit microService2Refit) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cityList = await microService2Refit.GetCityList();
            var cityList3 = await microService2Refit.GetCityList2();

            var cityList2 = await microService2Service.GetCityList();


            return Ok(new { cityList = cityList, cityList2 = cityList2 });
        }
    }
}