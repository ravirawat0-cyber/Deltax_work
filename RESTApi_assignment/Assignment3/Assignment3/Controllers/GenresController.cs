using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Models.Request;
using Assignment3.Services;
using Assignment3.Services.Interfaces;
using System;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
                var genre = _genreServices.GetAll();
                return Ok(genre);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
                var genre = _genreServices.GetById(id);
                return Ok(genre);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreRequest request)
        {
                var id = _genreServices.Create(request);
                return Ok(id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody] GenreRequest request)
        {
                _genreServices.Update(id, request);
                return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {

                _genreServices.Delete(id);
                return Ok();
        }
    }
}
