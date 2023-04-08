using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services.Interfaces;
using System;

namespace RESTApi_assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _movieServices;

        public MoviesController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var movie = _movieServices.GetAll();
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var movie = _movieServices.GetById(id);
                return Ok(movie);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
      
         
        }

        [HttpPost]
        public IActionResult Create([FromBody] MovieRequest request)
        {
            try
            {
                int id = _movieServices.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = id }, request);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id,[FromBody] MovieRequest request)
        {
            try
            {
                int updated = _movieServices.Update(id, request);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _movieServices.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
