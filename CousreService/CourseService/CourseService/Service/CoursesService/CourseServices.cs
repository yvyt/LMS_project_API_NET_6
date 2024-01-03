using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace CourseService.Service.CoursesService
{
    public class CourseServices : ICourseService
    {
        private readonly CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IConfiguration _config;

        public CourseServices(CourseContext courseContext, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = courseContext;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
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
                    c.UpdatedAt = DateTime.UtcNow;
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

        public async Task<List<CourseDTO>> GetActiceCourse()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var courses = _context.Courses.Where(x => x.IsActive == true).ToList();
                    List<CourseDTO> result = new List<CourseDTO>();
                    foreach (var course in courses)
                    {
                        CourseDTO courseDTO = new CourseDTO
                        {
                            Name = course.Name,
                            Id = course.Id,
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

        public List<CourseDTO> GetAll()
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var courses = _context.Courses.ToList();
                    List<CourseDTO> result = new List<CourseDTO>();
                    foreach (var course in courses)
                    {
                        CourseDTO courseDTO = new CourseDTO
                        {
                            Name=course.Name,
                            Id=course.Id,
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



        public CourseDTO GetById(string id)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var c = _context.Courses.FirstOrDefault(co => co.Id == id);
                    if (c != null)
                    {
                        CourseDTO courseDTO = new CourseDTO
                        {
                            Name= c.Name,
                            Id=c.Id,
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
                    courses.UpdatedAt = DateTime.UtcNow;
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



    }
}
