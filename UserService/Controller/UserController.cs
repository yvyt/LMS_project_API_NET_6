using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;
using UserService.Service;

namespace UserService.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUser user, string role)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(user, role);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(user);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> Confirm(string email, string token)
        {
            var result = await _userService.ConfirmEmail(email, token);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("Test")]
        public async Task<IActionResult> Confirm(string email)
        {
            var result = await _userService.SendMailAsync(email);
           
            return Ok(result);
        }
    }
  
}
