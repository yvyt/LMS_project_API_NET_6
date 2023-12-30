﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
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
    }
  
}
