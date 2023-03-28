using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("movies/{movieId}/reviews")]
    public class ReviewsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(int movieId) => Ok();

        [HttpGet("{id:int}")]
        public IActionResult GetById(int movieId, int id) => Ok();

        [HttpPost]
        public IActionResult Create(int movieId) => StatusCode(201);

        [HttpPut("{id:int}")]
        public IActionResult Update(int movieId, int id) => Ok();

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int movieId, int id) => Ok();
    }
}
