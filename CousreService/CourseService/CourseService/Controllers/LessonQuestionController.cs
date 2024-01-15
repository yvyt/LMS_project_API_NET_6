using CourseService.Data;
using CourseService.Model;
using CourseService.Service.LessonQuestionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonQuestionController : ControllerBase
    {
        private ILessonQuestionService _service;
        private CourseContext _context;

        public LessonQuestionController(ILessonQuestionService service,CourseContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpPost("AddQuestionFromStudent")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Student")]
        public async Task<IActionResult> AddLessonQuestion(LessonQuestionDTO questionDTO)
        {
            var result = await _service.AddLessonQuestion(questionDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddQuestionFromTeacher")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> AddLessonQuestionFromTeacher(LessonQuestionFromTeacher questionDTO)
        {
            var result = await _service.AddLessonQuestionFromTeacher(questionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetQuestionByTime")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> GetQuestionByTime()
        {
            var result = await _service.GetQuestionByTime();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("GetQuestionByAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> GetQuestionByAnswer()
        {
            var result = await _service.GetQuestionByAnswer();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
