using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost("AddDocment")]
        public async Task<IActionResult> AddDocument(DocumentDTO d)
        {
            var result = await _documentService.AddDocument(d);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
