using ExamService.Data;
using ExamService.Model;
using ExamService.Service.AnswerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        [HttpPost("AddAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateAnswer")]
        public async Task<IActionResult> AddAnswer(AnswerDTO answerDTO)
        {
            var result = await _answerService.AddAnswer(answerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _answerService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewAnswer")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _answerService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByQuestion")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewAnswer")]
        public async Task<IActionResult> GetByQuestion(string id)
        {
            var result = await _answerService.GetByQuestion(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetCorrectAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewAnswer")]
        public async Task<IActionResult> GetCorrectAnswer(string questionId)
        {
            var result = await _answerService.GetCorrectAnswer(questionId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditAnswer")]
        public async Task<IActionResult> EditAnswer(AnswerDTO answerDTO)
        {
            var result = await _answerService.EditAnswer(answerDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteAnswer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteAnswer")]
        public async Task<IActionResult> DeleteAnswer(string id)
        {
            var result = await _answerService.DeleteAnswer(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewAnswer")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _answerService.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

