using ExamService.Model;
using ExamService.Service.ExamService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }
        [HttpPost("AddExam")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateExam")]
        public async Task<IActionResult> AddExam([FromForm] ExamDTO examDTO)
        {
            var result = await _examService.AddExam(examDTO);
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
            var result = await _examService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewExam")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _examService.GetById(id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditExams")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditExam")]
        public async Task<IActionResult> EditExam([FromForm] ExamDTO examDTO)
        {
            var result = await _examService.EditExam(examDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteExams")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteExam")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var result = await _examService.DeleteExam(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        [Authorize(Policy = "ViewExam")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _examService.GetActive();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("ApproveResource")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]
        public async Task<IActionResult> ApproveExam(string id)
        {
            var result = await _examService.ApproveExam(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
    }
}
