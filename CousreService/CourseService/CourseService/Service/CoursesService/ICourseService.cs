using CourseService.Data;
using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.CoursesService
{
    public interface ICourseService
    {
        public Task<ManagerRespone> AddCourse(CourseDTO course);
        Task<ManagerRespone> DeleteCoure(string id);
        public List<Course> GetAll();
        Course GetById(string id);
        Task<ManagerRespone> UpdateCourse(string id, CourseDTO course);
    }
}
