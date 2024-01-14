using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
namespace CourseService.Service.StudentCourseService
{
    public class StudentCourseService : IStudentCourseService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public StudentCourseService(CourseContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddStudent(StudentCourseDTO dTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == dTO.Class);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class with id=${dTO.Class}",
                            IsSuccess = false,
                        };
                    }
                    StudentCourse studentCourse = new StudentCourse
                    {
                        ClassId = dTO.Class,
                        StudentId = user.Id
                    };

                    await _context.AddAsync(studentCourse);
                    var number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when add student to class",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully saved {number} changes to the database.",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unthorize",
                IsSuccess = false,
            };
        }
        public async Task<UserDTO> GetUsersFromUserServiceAsync(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://localhost:44357/User/UserById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var jsonContent = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
            return null;
        }

        public async Task<List<StudentCourseDetail>> GetStudentByClass(string classId)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == classId);
                    if (classes == null)
                    {
                        return null;
                    }
                    var students = await _context.StudentCourses.Where(s=>s.ClassId==classId).ToListAsync();
                    List<StudentCourseDetail> result = new List<StudentCourseDetail>();
                    foreach(var student in students)
                    {
                        var st= await GetUsersFromUserServiceAsync(student.StudentId,accessToken);
                        if (st != null)
                        {
                            StudentCourseDetail detail = new StudentCourseDetail
                            {
                                id=student.Id,
                                Class = classes.Name,
                                Student = st.UserName
                            };
                            result.Add(detail);
                        }
                    }
                    return result;
                    
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> ExistClass(string classID)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == classID);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class with id=${classID}",
                            IsSuccess = false,
                        };
                    }
                    var students = await _context.StudentCourses.FirstOrDefaultAsync(s => s.ClassId == classID && s.StudentId == user.Id);
                    if (students == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not in this class",
                            IsSuccess = false,
                        };
                    }
                    _context.StudentCourses.Remove(students);
                    var number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when add student to class",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully exist class.",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unthorize",
                IsSuccess = false,
            };
        }

        public async Task<List<StudentCourseDetail>> CurrentClass()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    
                    var students = await _context.StudentCourses.Where(s => s.StudentId == user.Id).ToListAsync();
                    List<StudentCourseDetail> result = new List<StudentCourseDetail>();
                    foreach (var student in students)
                    {
                        var st = await GetUsersFromUserServiceAsync(student.StudentId, accessToken);
                        var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == student.ClassId);
                        if (st != null)
                        {
                            StudentCourseDetail detail = new StudentCourseDetail
                            {
                                id = student.Id,
                                Class = classes.Name,
                                Student = st.UserName
                            };
                            result.Add(detail);
                        }
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
