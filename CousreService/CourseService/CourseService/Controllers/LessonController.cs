using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using CourseService.Service.LessonService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using CourseService.Model;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService) {
            _lessonService = lessonService;
        }
        [HttpPost("create")]
        public  async Task<IActionResult> CreateLesson([FromForm] LessonDTO lessonModel)
        {
            try
            {
                var createdLesson =await _lessonService.AddLesson(lessonModel);
                if(createdLesson.IsSuccess)
                {
                    return Ok(createdLesson);

                }
                return BadRequest(createdLesson);
                
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _lessonService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByTopic")]
        public async Task<IActionResult> GetByTopic(string id)
        {
            var result = await _lessonService.GetByTopic(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditTopic")]
        public async Task<IActionResult> EditLesson(string id, [FromForm] LessonDTO lessonDTO)
        {
            try
            {
                var result = await _lessonService.EditLesson(id,lessonDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);

                }
                return BadRequest(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteLesson")]
        public async Task<IActionResult> DeleteLesson(string id)
        {
            var result = await _lessonService.DeleteLesson(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActiveLesson")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _lessonService.GetActiveLesson();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _lessonService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
