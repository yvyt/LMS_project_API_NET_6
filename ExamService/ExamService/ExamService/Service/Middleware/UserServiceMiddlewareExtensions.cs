using ExamService.Service.Middleware;

namespace ExamService.Service.Extentions
{
    public static class UserServiceMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserServiceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserServiceMiddleware>();
        }
    }
}
