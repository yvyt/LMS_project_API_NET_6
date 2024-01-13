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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateClass")]

        public async Task<IActionResult> AddClasses(ClassDTO classDTO)
        {
            var result = await _classesService.AddClasses(classDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("ManagerClass")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewClass")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _classesService.GetById(id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditClasses")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditClass")]
        public async Task<IActionResult> EditClass(string id, ClassDTO classDTO)
        {
            var result = await _classesService.EditClass(id,classDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteClass")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteClass")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var result = await _classesService.DeleteClass(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActiveClass")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewClass")]
        public async Task<IActionResult> GetActiveClass()
        {
            var result = await _classesService.GetActiveClasses();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetDetails")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewClass")]
        public async Task<IActionResult> GetDetailClasses(string id)
        {
            var result = await _classesService.GetDetailClass(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetCurrentClass")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Teacher")]
        [Authorize(Policy = "ViewClass")]
        public async Task<IActionResult> GetCurrentClass()
        {
            var result = await _classesService.GetCurrentClass();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
