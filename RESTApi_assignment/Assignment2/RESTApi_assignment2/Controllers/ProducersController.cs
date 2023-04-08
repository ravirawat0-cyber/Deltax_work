using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services;
using RESTApi_assignment2.Services.Interfaces;
using System;

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
                return Ok(producer);
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
                var producer = _producerServices.GetById(id);
                return Ok(producer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _producerServices.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
