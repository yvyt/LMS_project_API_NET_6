using CourseService.Model;

namespace CourseService.Service.StudentCourseService
{
    public interface IStudentCourseService
    {
        Task<ManagerRespone> AddStudent(StudentCourseDTO dTO);
        Task<ManagerRespone> ExistClass(string classID);
        Task<List<StudentCourseDetail>> GetStudentByClass(string classId);
    }
}
