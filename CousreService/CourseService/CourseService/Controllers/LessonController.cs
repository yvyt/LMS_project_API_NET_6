using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using CourseService.Service.LessonService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using UserService.Model;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly CourseContext _context;
        public LessonController(ILessonService lessonService,CourseContext context) {
            _lessonService = lessonService;
            _context = context;
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
    }
}
