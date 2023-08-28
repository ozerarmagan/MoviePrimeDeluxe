using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.Business.Concrete;
using MoviePrimeDeluxe.Entities;

namespace MoviePrimeDeluxe.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie != null) 
            {
                return Ok(movie);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetMovieByName(string name)
        {
            var movie = await _movieService.GetMovieByName(name);
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateMovie([FromBody]Movie movie) 
        {
            var createdMovie = await _movieService.CreateMovie(movie);
            return CreatedAtAction("Get", new {id = createdMovie.Id}, createdMovie);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateMovie([FromBody] Movie movie)
        {
            if (await _movieService.GetMovieById(movie.Id) != null)
            {
                return Ok(await _movieService.UpdateMovie(movie));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteMovie(int id) 
        {
            if (await _movieService.GetMovieById(id) != null)
            {
                await _movieService.DeleteMovie(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
