using ExamService.Model;
using ExamService.Service.QuestionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost("AddQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateQuestion")]
        public async Task<IActionResult> AddQuestion(QuestionDTO questionDTO)
        {
            var result = await _questionService.AddQuestion(questionDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _questionService.GetAll();
            if(result !=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewQuestion")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _questionService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditQuestion")]
        public async Task<IActionResult> EditQuestion(QuestionDTO questionDTO)
        {
            var result = await _questionService.EditQuestion(questionDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var result = await _questionService.DeleteQuestion(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewQuestion")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _questionService.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
