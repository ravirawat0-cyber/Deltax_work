using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Models.Request;
using Assignment3.Services.Interfaces;
using System;
using Microsoft.AspNetCore.Routing;
using Firebase.Storage;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Auth;
using System.Drawing;
using System.Net;

namespace Assignment3.Controllers
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
                var movie = _movieServices.GetAll();
                return movie.Count == 0 ? NoContent() : Ok(movie);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
                var movie = _movieServices.GetById(id);
                return Ok(movie);
        }

         [HttpPost]
         public IActionResult Create([FromBody] MovieRequest request)
         {    
                 var id = _movieServices.Create(request);
                 return Ok(id);
         }
        
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id,[FromBody] MovieRequest request)
        {
                _movieServices.Update(id, request);
                return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
                _movieServices.Delete(id);
                return Ok();
        }

        [HttpPost("uploads")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("file not selected");

            var imageUrl = await new FirebaseStorage("imdb-88f70.appspot.com")
                    .Child(Guid.NewGuid().ToString() + ".jpg")
                    .PutAsync(file.OpenReadStream());
            if(imageUrl == null)
            {
                return StatusCode(204, "No image is uploded to Firebase");
            }
            return Ok(imageUrl);
        }
    }
}
