using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using UserService.Data;
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
        [HttpPost("LoginWithOTP")]
        public async Task<IActionResult> LoginWithOTP(string otp, string email)
        {
            var result= await _userService.LoginWithOTP(otp, email);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("UserManager")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewUser")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            if(result!=null)
            {
                return Ok(result);
            }
            return Ok(new UserManagerRespone
            {
                Message = "Don't have any user"
            }) ;
        }
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var result = await _userService.ForgotPassword(email);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("ResetPassword")]
        public IActionResult GetResetPassword(string token, string email)
        {
            var result = _userService.GetResetPassword(token, email);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var result =await _userService.ResetPassword(model);
            if (result.IsSuccess)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("UserById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userService.GetUser(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new UserManagerRespone
            {
                Message = "Don't have user with: "+id
            });
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut()
        {
            return Ok(_userService.LogOut());
        }
        [HttpGet("UserByToken")]

        public async Task<IActionResult> GetByToken(string token)
        {
            var result = await _userService.GetUserByToken(token);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new UserManagerRespone
            {
                Message = "Don't have user with: " + token
            });
        }
        [HttpPost("CreateUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUser user, string role)
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
        [HttpPost("AddRole")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
        public async Task<IActionResult> CreateRole([FromForm] RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddRole(roleDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpGet("GetUserRolePermission")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]
        public async Task<IActionResult> GetUserRolePermission(string id)
        {
            var result = await _userService.GetRolePermission(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut("EditRolePermission")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]
        public async Task<IActionResult> EditRolePermission(string id,[FromForm] RoleDTO dto)
        {
            var result = await _userService.EditRolePermission(id,dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }

}
 