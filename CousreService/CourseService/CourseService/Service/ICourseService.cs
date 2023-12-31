using CourseService.Model;
using UserService.Model;

namespace CourseService.Service
{
    public interface ICourseService
    {
        public Task<UserManagerRespone> AddCourse(Course course);
    }
}
