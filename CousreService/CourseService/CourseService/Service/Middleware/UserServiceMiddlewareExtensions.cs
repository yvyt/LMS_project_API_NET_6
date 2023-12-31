using CourseService.Service.Middleware;

namespace CourseService.Service.Extentions
{
    public static class UserServiceMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserServiceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserServiceMiddleware>();
        }
    }
}
