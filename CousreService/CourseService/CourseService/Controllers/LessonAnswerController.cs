using CourseService.Model;
using CourseService.Service.LessonAnswerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonAnswerController : ControllerBase
    {
        private ILessonAnswerService _service;

        public LessonAnswerController(ILessonAnswerService service)
        {
            _service = service;
        }
        [HttpPost("AddLessonAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> AddLessonAnswer(LessonAnswerDTO answerDTO)
        {
            var result = await _service.AddLessonAnswer(answerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAnswerByQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetByQuestion(string questionId)
        {
            var result = await _service.GetByQuestion(questionId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditLessonAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Teacher")]
        public async Task<IActionResult> EditAnswer(string id,LessonAnswerDTO answerDTO)
        {
            var result = await _service.EditLessonAnswer(id,answerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> DeleteAnswer(string id)
        {
            var result = await _service.DeleteAnswer(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
