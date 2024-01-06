using CourseService.Data;
using CourseService.Model;
using CourseService.Model;

namespace CourseService.Service.LessonService
{
    public interface ILessonService
    {
        Task<ManagerRespone> AddLesson(LessonDTO lessonDTO);
        Task<ManagerRespone> DeleteLesson(string id);
        Task<ManagerRespone> EditLesson(string id, LessonDTO lessonDTO);
        Task<List<LessonDTO>> GetActiveLesson();
        Task<List<LessonDTO>> GetAll();
        Task<LessonDTO> GetById(string id);
        Task<List<LessonDTO>> GetByTopic(string id);
    }
}
