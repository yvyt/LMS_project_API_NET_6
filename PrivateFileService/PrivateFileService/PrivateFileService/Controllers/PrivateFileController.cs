using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateFileService.Data;
using PrivateFileService.Model;
using PrivateFileService.Service.PrivateFileService;
using System.Net.Http.Headers;

namespace PrivateFileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateFileController : ControllerBase
    {
        private readonly IPrivateFileService _privateFileService;
        public PrivateFileController(IPrivateFileService privateFileService)
        {
            _privateFileService = privateFileService;
        }
        [HttpPost("AddPrivateFile")]
        public async Task<IActionResult> AddPrivateFile([FromForm] PrivateFileUploadDTO privateFile)
        {
            var result = await _privateFileService.AddPrivateFile(privateFile);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllFile")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _privateFileService.GetAll();
            if (result !=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _privateFileService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("Rename")]
        public async Task<IActionResult> RenameFile(string id, string newName)
        {
            var result = await _privateFileService.RenameFile(id,newName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeletePF")]
        public async Task<IActionResult> DeletePF(string id)
        {
            var result = await _privateFileService.DeletePF(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _privateFileService.GetActive();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("DownloadPF")]
        public async Task<IActionResult> DownloadPF(string id)
        {
            var (fileStream, message) = await _privateFileService.DownloadPF(id);
            if (fileStream!=null)
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
