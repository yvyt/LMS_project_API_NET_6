using CourseService.Model;
using CourseService.Service.StudentCourseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private IStudentCourseService _service;

        public StudentCourseController(IStudentCourseService service)
        {
            _service = service;
        }
        [HttpPost("AddStudent")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Student")]

        public async Task<IActionResult> AddStudent(StudentCourseDTO dTO)
        {
            var result = await _service.AddStudent(dTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetStudentByClass")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetByClass(string classId)
        {
            var result = await _service.GetStudentByClass(classId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("ExitClass")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Student")]

        public async Task<IActionResult> ExitClass(string classID)
        {
            var result = await _service.ExistClass(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
