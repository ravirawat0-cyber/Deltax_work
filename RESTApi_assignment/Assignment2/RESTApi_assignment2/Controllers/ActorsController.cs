using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace RESTApi_assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorServices _actorServices;

        public ActorsController(IActorServices actorServices)
        {
            _actorServices = actorServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var actor = _actorServices.GetAll();
                return Ok(actor);
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
                var actor = _actorServices.GetById(id);
                return Ok(actor);
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
        public IActionResult Create([FromBody] ActorRequest request)
        {
            try
            {
                int id  = _actorServices.Create(request);
                return CreatedAtAction(nameof(GetById), new {id = id}, request);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, [FromBody] ActorRequest request)
        {
            try
            {
                int updated = _actorServices.Update(id, request);
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
                _actorServices.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }  
        }
    }
}
