using CourseService.Data;
using CourseService.Model;
using CourseService.Service.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateResource")]
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
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Leadership")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewResource")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewResource")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditResource")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteResource")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewResource")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _resourceService.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return  BadRequest(result);
        }
        [HttpGet("DownloadResource")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewResource")]
        public async Task<IActionResult> DownloadPF(string id)
        {
            var (fileStream, message) = await _resourceService.DownloadResource(id);
            if (fileStream != null)
            {
                var contentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = message,
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(fileStream, "application/octet-stream", message);
            }
            return BadRequest(message);
        }
    }
}
