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
                    return new UserManagerRespone
                    {
                        Message = $"Successfully saved {numberOfChanges} changes to the database.",
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

        private JwtSecurityToken DecodeJwtToken(string token, SymmetricSecurityKey key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                ValidateIssuer = true,
                ValidIssuer = _config["AuthSettings:Issuer"],

                ValidateAudience = true,
                ValidAudience = _config["AuthSettings:Audience"], 
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero 
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            return securityToken as JwtSecurityToken;
        }
    }
}
