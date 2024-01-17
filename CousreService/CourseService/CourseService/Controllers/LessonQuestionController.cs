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

        public LessonQuestionController(ILessonQuestionService service)
        {
            _service = service;
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
        [HttpPut("EditQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> EditQuestionFromTeacher(string id,LessonQuestionFromTeacher questionDTO)
        {
            var result = await _service.EditQuestionFromTeacher(id,questionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher,Student")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var result = await _service.DeleteQuestion(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher,Student")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActive();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByLesson")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Student")]
        public async Task<IActionResult> GetByLesson(string lessonId)
        {
            var result = await _service.GetByLesson(lessonId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("LikeQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> LikeQuestion(string id)
        {
            var result = await _service.LikeQuestion(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
