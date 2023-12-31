using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Model;

namespace CourseService.Service
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

        public async Task<UserManagerRespone> AddCourse(Model.Course course)
        {

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    Data.Course co = new Data.Course
                    {
                        Name = course.Name,
                        userId = user.Id,
                    };
                    _context.Courses.Add(co);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if(numberOfChanges > 0)
                    {
                        return new UserManagerRespone
                        {
                            Message = $"Successfully saved {numberOfChanges} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new UserManagerRespone
                    {
                        Message = $"Error when create new course.",
                        IsSuccess = true,
                    };

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return new UserManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new UserManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false,
            };

        }

        public async Task<UserManagerRespone> DeleteCoure(string id)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var c = await _context.Courses.FirstOrDefaultAsync(cs => cs.Id == id);
                    if (c == null)
                    {
                        return new UserManagerRespone
                        {
                            Message = $"Don't have course with id={id}",
                            IsSuccess = false
                        };
                    }
                    c.IsDeleted = true;
                    c.UpdatedAt = DateTime.UtcNow;
                    var numberChange = await _context.SaveChangesAsync();
                    if (numberChange > 0)
                    {
                        return new UserManagerRespone
                        {
                            Message = $"Delete course successfully",
                            IsSuccess = true
                        };
                    }

                }
                catch (Exception ex)
                {
                    return new UserManagerRespone
                    {
                        Message = $"Error when delete: {ex.Message}",
                        IsSuccess = false
                    };
                }
            }
            return  new UserManagerRespone
            {
                Message = $"Unauthorized",
                IsSuccess = false
            }; ;
        }

        public List<Data.Course> GetAll()
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var courses= _context.Courses.ToList();
                    return courses;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

      

        public Data.Course GetById(string id)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var c = _context.Courses.FirstOrDefault(co => co.Id == id);
                    if (c != null)
                    {
                        return c;
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

        public async Task<UserManagerRespone> UpdateCourse(string id, Model.Course course)
        {
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO;
            if (user != null)
            {
                try
                {
                    var courses = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
                    if (courses == null)
                    {
                        return new UserManagerRespone
                        {
                            Message = $"Don't have course with id={id}",
                            IsSuccess = false
                        };
                    }
                    courses.Name=course.Name;
                    courses.UpdatedAt = DateTime.UtcNow;
                    var numberColumnsChange= await _context.SaveChangesAsync();
                    if (numberColumnsChange > 0)
                    {
                        return new UserManagerRespone
                        {
                            Message = $"Successfully update course",
                            IsSuccess = true,
                        };
                    }
                    return new UserManagerRespone
                    {
                        Message = $"Error when update course",
                        IsSuccess = false,
                    };

                }
                catch (Exception ex)
                {
                    return new UserManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new UserManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false,
            };

        }


        
    }
}
