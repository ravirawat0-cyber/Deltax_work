using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace RESTApi_assignment2.Controllers
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
            try
            {
                var genre = _genreServices.GetAll();
                return Ok(genre);
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
                var genre = _genreServices.GetById(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreRequest request)
        {
            try
            {
                int id = _genreServices.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = id }, request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody] GenreRequest request)
        {
            try
            {
                int updated = _genreServices.Update(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _genreServices.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
