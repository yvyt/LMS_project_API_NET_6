using CourseService.Data;
using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.CoursesService
{
    public interface ICourseService
    {
        public Task<ManagerRespone> AddCourse(CourseDTO course);
        Task<ManagerRespone> DeleteCoure(string id);
        public List<CourseDTO> GetAll();
        CourseDTO GetById(string id);
        Task<ManagerRespone> UpdateCourse(string id, CourseDTO course);
        Task<List<CourseDTO>> GetActiceCourse();
    }
}
