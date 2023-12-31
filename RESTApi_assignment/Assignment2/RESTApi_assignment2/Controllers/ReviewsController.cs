﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace RESTApi_assignment2.Controllers
{
    [Route("api/[controller]/{movieId}")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewsController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }
        [HttpGet]
        public IActionResult GetAll([FromRoute]int movieId)
        {
            try
            {
                var review = _reviewServices.GetAll(movieId);
                return review.Count == 0 ? NoContent() : Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception) 
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute]int movieId, [FromRoute] int id)
        {
            try
            {
                var review = _reviewServices.GetById(movieId, id);
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception) 
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPost]
        public IActionResult Create([FromRoute]int movieId, [FromBody] ReviewRequest request)
        {
            try
            {
                int id = _reviewServices.Create(movieId, request);
                var review = _reviewServices.GetById(movieId, id);
                return CreatedAtAction(nameof(GetById), new { movieId = movieId, id = id }, review);
         
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int movieId, [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            try
            {
                int updated = _reviewServices.Update(movieId, id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute]int movieId,[FromRoute] int id)
        {
            try
            {
                _reviewServices.Delete(movieId, id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception) 
            { 
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
