using CourseService.Service.UserServiceClinet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseService.Model;
using Microsoft.AspNetCore.Authorization;
using CourseService.Service.CoursesService;

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
        [Authorize(Policy = "CreateCourse")]
        public async Task<IActionResult> AddCourse(CourseDTO c)
        {
            var result = await _courseService.AddCourse(c);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("AllCourses")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]
        public async Task<IActionResult> AllCourse()
        {
            var result = await _courseService.GetAll();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("CourseById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewCourse")]
        public async Task<IActionResult> GetCourse(string id)
        {
            var result =  await _courseService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditCourse")]
        public async Task<IActionResult> Update(string id,Model.CourseDTO course)
        {
            var result = await _courseService.UpdateCourse(id,course);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteCourse")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _courseService.DeleteCoure(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActiveCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewCourse")]
        public async Task<IActionResult> GetActiveCourse()
        {
            var result = await _courseService.GetActiceCourse();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
