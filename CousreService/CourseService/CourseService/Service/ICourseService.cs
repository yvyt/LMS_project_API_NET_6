using CourseService.Data;
using CourseService.Model;
using UserService.Model;

namespace CourseService.Service
{
    public interface ICourseService
    {
        public Task<UserManagerRespone> AddCourse(Model.Course course);
        Task<UserManagerRespone> DeleteCoure(string id);
        public List<Data.Course> GetAll();
        Data.Course GetById(string id);
        Task<UserManagerRespone> UpdateCourse(string id, Model.Course course);
    }
}
