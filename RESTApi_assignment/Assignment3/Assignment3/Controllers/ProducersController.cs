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
                var producer = _producerServices.GetAll();
                return Ok(producer);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
                var producer = _producerServices.GetById(id);
                return Ok(producer);
        }

        [HttpPost]
        public IActionResult Create(ProducerRequest request)
        {
                var id = _producerServices.Create(request);
                return Ok(id);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
                _producerServices.Update(id, request);
                return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
                _producerServices.Delete(id);
                return Ok();
        }
    }
}
