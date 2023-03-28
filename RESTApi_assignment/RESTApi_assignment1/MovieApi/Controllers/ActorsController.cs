using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("actors")]
    public class ActorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok();

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => Ok();

        [HttpPost]
        public IActionResult Create() => StatusCode(201);

        [HttpPut("{id:int}")]
        public IActionResult Update(int id) => Ok();

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) => Ok();
    }
}
