using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePrimeDeluxe.Business.Abstract;
using MoviePrimeDeluxe.Business.Concrete;
using MoviePrimeDeluxe.Entities.Models;
using MoviePrimeDeluxe.Entities;
using System.Data;

namespace MoviePrimeDeluxe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister user)
        {
            var response = await _authService.Register(new Entities.User
            {
                Username = user.Username
            }, user.Password);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin user)
        {
            var result = await _authService.Login(user.Username, user.Password);
            if (result == null)
            {
                return BadRequest("Fail");
            }
            return Ok(result);
        }

        [HttpPut("changePassword")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword user)
        {
            var result = await _authService.ChangePassword(user.OldPassword, user.NewPassword, user.ConfirmPassword);
            return Ok(result);
        }

        //[HttpPut("user-update")]
        //[Authorize]
        //public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(UserUpdate user)
        //{
        //    var result = await _authService.UpdateUser(user.Username, user.OldPassword, user.NewPassword, user.ConfirmPassword);
        //    return Ok(result);
        //}

        [HttpPut("roleForAdmin/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> RoleAdmin([FromRoute] string username)
        {
            var result = await _authService.RoleForAdmin(username);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteAccount(string password)
        {
            var result = await _authService.DeleteAccount(password);
            return Ok(result);
        }
    }
}
