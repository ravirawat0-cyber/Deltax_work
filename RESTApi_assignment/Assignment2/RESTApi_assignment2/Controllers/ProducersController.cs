using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace RESTApi_assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerServices _producerServices;

        public ProducersController(IProducerServices producerServices)
        {
            _producerServices = producerServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var producer = _producerServices.GetAll();
                return producer.Count == 0 ? NoContent() : Ok(producer);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var producer = _producerServices.GetById(id);
                return Ok(producer);
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
        public IActionResult Create(ProducerRequest request)
        {
            try
            {
                int id = _producerServices.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = id }, request);
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
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            try
            {
                int updated = _producerServices.Update(id, request);
                return Ok(updated);
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
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _producerServices.Delete(id);
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
