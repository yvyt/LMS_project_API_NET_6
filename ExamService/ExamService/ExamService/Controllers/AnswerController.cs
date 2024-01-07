using ExamService.Service.AnswerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private IAnswerService _answerService {  get; set; }
        public AnswerController(AnswerService answerService)
        {
            _answerService = answerService;
        }
    }
}
