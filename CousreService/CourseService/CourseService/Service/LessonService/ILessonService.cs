using CourseService.Data;
using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.LessonService
{
    public interface ILessonService
    {
        Task<ManagerRespone> AddLesson(LessonDTO lessonDTO);
        Task<List<LessonDTO>> GetAll();
    }
}
