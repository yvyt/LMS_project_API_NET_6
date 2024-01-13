using CourseService.Data;
using CourseService.Model;
using CourseService.Service.TopicService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [HttpPost("AddTopic")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateTopic")]

        public async Task<IActionResult> AddTopic(TopicDTO topicDTO)
        {
            var result = await _topicService.AddTopic(topicDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("ManagerTopic")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Leadership")]
        public async Task<IActionResult> ManagerTopic()
        {
            var result = await _topicService.GetAll();
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("TopicById")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewTopic")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _topicService.GetById(id);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("TopicByClass")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewTopic")]
        public async Task<IActionResult> GetByClass(string classId)
        {
            var result = await _topicService.GetByClass(classId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditTopic")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditTopic")]

        public async Task<IActionResult> EditTopic (string id,TopicDTO topic)
        {
            var result = await _topicService.Edit(id,topic);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteTopic")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "DeleteTopic")]
        public async Task<IActionResult> DeleteTopic(string id)
        {
            var result = await _topicService.DeleteTopic(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetActiveTopic")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewTopic")]
        public async Task<IActionResult> GetActiceTopic()
        {
            var result = await _topicService.GetActiveTopic();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
