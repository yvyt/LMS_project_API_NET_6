using CourseService.Model;
using CourseService.Service.ClassesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private IClassService _classesService;

        public ClassesController(IClassService classService) {
            _classesService = classService;
        }
        [HttpPost("AddClasses")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]

        public async Task<IActionResult> AddClasses(ClassDTO classDTO)
        {
            var result = await _classesService.AddClasses(classDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllClass")]
        public async Task<IActionResult> GetAllClasses()
        {
            var result = await _classesService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetClassById")]

        public async Task<IActionResult> GetById(string id)
        {
            var result = await _classesService.GetById(id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
