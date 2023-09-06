using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.Business.Concrete;
using MoviePrimeDeluxe.Entities;
using MoviePrimeDeluxe.Validation;

namespace MoviePrimeDeluxe.API.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        //private readonly IValidator<Movie> _movieValidator;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
            //_movieValidator = movieValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if(movies == null)
            {
                return Ok("No movie here.");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Movie id can not less than 1.");
            }

            var movie = await _movieService.GetMovieById(id);

            if (movie == null) 
            {
                return NotFound("Movie can not found.");
            }  
            
            return Ok(movie);
        }

        [HttpGet]
        [Authorize]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetMovieByName(string name)
        {
            var movie = await _movieService.GetMovieByName(name);
            if (movie == null)
            {
                return NotFound("Movie can not found");
            }
            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("[action]")]
        public async Task<IActionResult> CreateMovie([FromBody]Movie movie)
        {
            var list = new List<string>();
            list.Add("Id");

            var validator = new MovieValidator(list);
            var validationResult = await validator.ValidateAsync(movie);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var createdMovie = await _movieService.CreateMovie(movie);
            return Ok(createdMovie);
            
            //return CreatedAtAction("Get", new { id = createdMovie.Id }, createdMovie);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateMovie([FromBody] Movie movie)
        {
            var list = new List<string>();
            var validator = new MovieValidator(list);
            var validationResult = await validator.ValidateAsync(movie);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            return Ok(await _movieService.UpdateMovie(movie));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteMovie(int id) 
        {
            if (await _movieService.GetMovieById(id) == null)
            {
                return NotFound();
            }
            await _movieService.DeleteMovie(id);
            return Ok();
        }
    }
}
