using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.DataAccess.Migrations;
using MoviePrimeDeluxe.Entities;

namespace MoviePrimeDeluxe.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost("[action]/{userId}/watched/{movieId}")]
        public async Task<IActionResult> MarkMovieAsWatched(int userId, int movieId, bool isWatched)
        {
            await _userService.MarkMovieAsWatched(userId, movieId, isWatched);
            return Ok();
        }

        [HttpGet("[action]/{userId}/watched")]
        public async Task<IActionResult> GetWatchedMoviesForUser(int userId)
        {
            var user = await _userService.GetWatchedMoviesForUser(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (await _userService.GetUserById(user.Id) != null)
            {
                return Ok(await _userService.UpdateUser(user));
            }
            return NotFound();
        }
    }
}
