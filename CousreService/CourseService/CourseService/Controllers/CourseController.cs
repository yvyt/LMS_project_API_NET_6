using CourseService.Service;
using CourseService.Service.UserServiceClinet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseService.Model;
using Microsoft.AspNetCore.Authorization;

namespace CourseService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpPost("AddCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddCourse(Course c)
        {
            var result = await _courseService.AddCourse(c);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
    }
}
