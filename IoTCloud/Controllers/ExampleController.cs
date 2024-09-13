using Microsoft.AspNetCore.Mvc;

namespace IoTCloud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [HttpGet("search1")]
        public IActionResult Search(string name, int year)
        {
            Console.WriteLine($"name={name}, year={year}");
            return Ok();
        }

        [HttpGet("search2")]
        public IActionResult Search()
        {
            var name = HttpContext.Request.Query["name"];
            var year = HttpContext.Request.Query["year"];
            Console.WriteLine($"name={name}, year={year}");
            return Ok();
        }

        public class MovieQuery
        {
            public string Name { get; set; }
            public int Year { get; set; }
        }

        [HttpGet("search3")]
        public IActionResult Search([FromQuery] MovieQuery movieQuery)
        {
            Console.WriteLine($"name={movieQuery.Name}, year={movieQuery.Year}");
            return Ok();
        }
    }
}
