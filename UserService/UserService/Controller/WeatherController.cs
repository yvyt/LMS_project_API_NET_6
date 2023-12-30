using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Model;

namespace UserService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

       
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]


        public IActionResult Get()
        {

            var result = "You are login";
            return Ok(result);
        }

    }
}
