using CourseService.Data;
using CourseService.Model;
using CourseService.Model;

namespace CourseService.Service.CoursesService
{
    public interface ICourseService
    {
        public Task<ManagerRespone> AddCourse(CourseDTO course);
        Task<ManagerRespone> DeleteCoure(string id);
        public Task<List<CourseDetail>> GetAll();
        Task<CourseDetail> GetById(string id);
        Task<ManagerRespone> UpdateCourse(string id, CourseDTO course);
        Task<List<CourseDetail>> GetActiceCourse();
    }
}
