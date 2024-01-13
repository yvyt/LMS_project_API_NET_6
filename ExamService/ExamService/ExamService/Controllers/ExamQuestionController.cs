using ExamService.Data;
using ExamService.Model;
using ExamService.Service.ExamQuestionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamQuestionController : ControllerBase
    {
        private readonly IExamQuestionService _service;
        public ExamQuestionController(IExamQuestionService service)
        {
            _service = service;
        }
        [HttpPost("AddExamQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateExamQuestion")]
        public async Task<IActionResult> AddExamQuestion([FromForm] ExamQuestionDTO examQuestionDTO)
        {
            var result = await _service.AddExamQuestion(examQuestionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewExamQuestion")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            if(result !=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByExam")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewExamQuestion")]
        public async Task<IActionResult> GetByExam(string examId)
        {
            var result = await _service.GetByExam(examId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditExamQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditExamQuestion")]
        public async Task<IActionResult> EditExamQuestion([FromForm] ExamQuestionUpdateDTO examQuestionDTO)
        {
            var result = await _service.EditExamQuestion(examQuestionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteExamQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteExamQuestion")]
        public async Task<IActionResult> DeleteExamQuestion(string id)
        {
            var result = await _service.DeleteExamQuestion(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewExamQuestion")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddMoreQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditExamQuestion")]
        public async Task<IActionResult> AddMoreQuestion([FromForm] QuestionExamAdd examQuestionDTO)
        {
            var result = await _service.AddMoreQuestion(examQuestionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
