using PrivateFileService.Service.Middleware;

namespace PrivateFileService.Service.Extentions
{
    public static class UserServiceMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserServiceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserServiceMiddleware>();
        }
    }
}
