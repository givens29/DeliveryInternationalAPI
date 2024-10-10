using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_DeliveryInternational.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _userService.Register(model);
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            try
            {
                var token = await _userService.Login(model);

                if (token != null)
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userEmailClaim = User.FindFirst(ClaimTypes.Email);
                if (userEmailClaim == null)
                {
                    return Unauthorized();
                }

                var editedUser = await _userService.Logout(userEmailClaim.Value);
                if (editedUser == false)
                {
                    return BadRequest();
                }
                return Ok("You've Logout!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<ActionResult<ProfileUserDto>> Profile()
        {

            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var userProfile = await _userService.Profile(emailClaim.Value);
                    return Ok(userProfile);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }
            }
            else
            {
                return Unauthorized();
            }

        }
        [HttpPut("editprofile")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> EditProfile([FromBody] EditUserProfileDto model)
        {
            try
            {
                var userEmailClaim = User.FindFirst(ClaimTypes.Email);
                if (userEmailClaim == null)
                {
                    return Unauthorized();
                }

                var editedUser = await _userService.EditProfile(userEmailClaim.Value, model);
                return Ok(editedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

      
    }
}
