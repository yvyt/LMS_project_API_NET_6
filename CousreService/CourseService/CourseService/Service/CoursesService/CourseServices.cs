using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using CourseService.Model;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace CourseService.Service.CoursesService
{
    public class CourseServices : ICourseService
    {
        private readonly CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        private IConfiguration _config;

        public CourseServices(CourseContext courseContext, IConfiguration config, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = courseContext;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddCourse(CourseDTO course)
        {

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    Course co = new Course
                    {
                        Name = course.Name,
                        userId = user.Id,
                    };
                    _context.Courses.Add(co);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully saved {numberOfChanges} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when create new course.",
                        IsSuccess = true,
                    };

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false,
            };

        }

        public async Task<ManagerRespone> DeleteCoure(string id)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var c = await _context.Courses.FirstOrDefaultAsync(cs => cs.Id == id);
                    if (c == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have course with id={id}",
                            IsSuccess = false
                        };
                    }
                    c.IsActive = false;
                    c.UpdatedAt = DateTime.Now;
                    _context.Courses.Update(c);
                    var numberChange = await _context.SaveChangesAsync();
                    if (numberChange > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Delete course successfully",
                            IsSuccess = true
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when delete: {ex.Message}",
                        IsSuccess = false
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unauthorized",
                IsSuccess = false
            }; ;
        }

        public async Task<List<CourseDetail>> GetActiceCourse()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var courses = _context.Courses.Where(x => x.IsActive == true).ToList();
                    List<CourseDetail> result = new List<CourseDetail>();
                    foreach (var course in courses)
                    {
                        var us = await GetUsersFromUserServiceAsync(course.userId, accessToken);
                        if (us == null)
                        {
                            return null;
                        }
                        CourseDetail courseDTO = new CourseDetail
                        {
                            Id = course.Id,
                            Name = course.Name,
                            CreateBy = us.UserName,
                            Status = course.Status,
                            CreatedAt=course.CreatedAt,
                            UpdatedAt=course.UpdatedAt,
                            IsActive = course.IsActive

                        };
                        result.Add(courseDTO);
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

        public async Task<List<CourseDetail>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var courses = _context.Courses.ToList();
                    List<CourseDetail> result = new List<CourseDetail>();
                    foreach (var course in courses)
                    {
                        var us = await GetUsersFromUserServiceAsync(course.userId, accessToken);
                        if (us == null)
                        {
                            return null;
                        }
                        CourseDetail courseDTO = new CourseDetail
                        {
                            Id = course.Id,
                            Name = course.Name,
                            CreateBy = us.UserName,
                            Status = course.Status,
                            CreatedAt = course.CreatedAt,
                            UpdatedAt = course.UpdatedAt,
                            IsActive=course.IsActive
                        };
                        result.Add(courseDTO);
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }



        public async Task<CourseDetail> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var c = _context.Courses.FirstOrDefault(co => co.Id == id);
                    if (c != null)
                    {
                        var us = await GetUsersFromUserServiceAsync(c.userId, accessToken);
                        if (us == null)
                        {
                            return null;
                        }
                        CourseDetail courseDTO = new CourseDetail
                        {
                            Id = c.Id,
                            Name = c.Name,
                            CreateBy = us.UserName,
                            Status = c.Status,
                            CreatedAt = c.CreatedAt,
                            UpdatedAt = c.UpdatedAt,
                            IsActive = c.IsActive

                        };
                        
                        return courseDTO;
                    }
                    return null;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> UpdateCourse(string id, CourseDTO course)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var courses = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
                    if (courses == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have course with id={id}",
                            IsSuccess = false
                        };
                    }
                    courses.Name = course.Name;
                    courses.UpdatedAt = DateTime.Now;
                    _context.Courses.Update(courses);
                    var numberColumnsChange = await _context.SaveChangesAsync();
                    if (numberColumnsChange > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully update course",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when update course",
                        IsSuccess = false,
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
                Message = $"Unauthorize",
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


    }
}
