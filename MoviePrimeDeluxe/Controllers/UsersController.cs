using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Business.Abstract;

namespace MoviePrimeDeluxe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        // private readonly IValidator<WatchedMovieValidator> _watchedMovieValidator;
        public UsersController(IUserService userService)
        //, IValidator<WatchedMovieValidator> watchedMovieValidator
        {
            _userService = userService;
         // _watchedMovieValidator = watchedMovieValidator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("[action]/{id}")]
        //public async Task<IActionResult> GetUserById(GetUserByIdDto userDto)
        public async Task<IActionResult> GetUserById(int id)
        {
            //var validator = new WatchedMovieValidator();
            //var validationResult = await validator.ValidateAsync(userDto);

            //{
            //    if (!validationResult.IsValid)
            //    {
            //        return BadRequest(validationResult.Errors);
            //    }
            //}

            //var user = await _userService.GetUserById(userDto.userId);
            //return Ok(user);

            if (id < 1)
            {
                return BadRequest("User id can not less than 1.");
            }

            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound("User can not be null.");
            }
            return Ok(user);
        }

        [HttpPost("[action]/{userId}/watched/{movieId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MarkMovieAsWatched(int userId, int movieId, bool isWatched)
        {

            if(userId < 1 || movieId < 1)
            {
                return BadRequest("User or movie id can not less than 1.");
            }

            await _userService.MarkMovieAsWatched(userId, movieId, isWatched);
            return Ok();

            /* 
            var validationResult = await _watchedMovieValidator.ValidateAsync();

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _userService.MarkMovieAsWatched(userId, movieId, isWatched);
            return Ok();
            */
        }

        [HttpGet("[action]/{userId}/watched")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetWatchedMoviesForUser(int userId)
        {
            if (userId < 1)
            {
                return BadRequest("User id can not less than 1.");
            }

            var user = await _userService.GetWatchedMoviesForUser(userId);

            if (user == null)
            {
                return NotFound("User can not be null.");
            }                 
            return Ok(user);
        }
    }
}

