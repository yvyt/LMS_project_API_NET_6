using CourseService.Model;
using CourseService.Model;

namespace CourseService.Service.TopicService
{
    public interface ITopicService
    {
        Task<ManagerRespone> AddTopic(TopicDTO topicDTO);
        Task<ManagerRespone> DeleteTopic(string id);
        Task<ManagerRespone> Edit(string id,TopicDTO topic);
        Task<List<TopicDTO>> GetActiveTopic();
        Task<List<TopicDTO>> GetAll();
        Task<List<TopicDTO>> GetByClass(string classId);
        Task<TopicDTO> GetById(string id);
    }
}
