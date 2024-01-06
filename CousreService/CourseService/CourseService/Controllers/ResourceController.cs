using CourseService.Data;
using CourseService.Model;
using CourseService.Service.ResourceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly CourseContext _context;
        public ResourceController(IResourceService resourceService,CourseContext context) {
            _resourceService=resourceService;
            _context=context;
        }
        [HttpPost("AddResource")]
        public async Task<IActionResult> AddResult([FromForm]  ResourceDTO resouceDTO)
        {
             var result = await _resourceService.AddResouce(resouceDTO);
             if (result.IsSuccess)
             {
                 return Ok(result);
             }
             return BadRequest(result);

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _resourceService.GetAll();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _resourceService.GetById(id);
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByLesson")]
        public async Task<IActionResult> GetByLesson(string id)
        {
            var result = await _resourceService.GetByLesson(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditResouce")]
        public async Task<IActionResult> Edit([FromForm] ResourceDTO resourceDTO)
        {
            var result = await _resourceService.EditResource(resourceDTO);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteResource")]
        public async Task<IActionResult> DeleteResource(string id)
        {
            var result = await _resourceService.DeleteResource(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActiveResource")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _resourceService.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return  BadRequest(result);
        }
    }
}
