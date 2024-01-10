using PrivateFileService.Service.UserServiceClinet;

namespace PrivateFileService.Service.Middleware
{
    public class UserServiceMiddleware
    {
        private readonly RequestDelegate _next;

        public UserServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserServiceClient userServiceClient)
        {
            // Extract access token from the request headers
            var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            if (string.IsNullOrEmpty(accessToken))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Access token is missing.");
                return;
            }
            // Call UserService API to get user information
            var user = await userServiceClient.GetUserDetailsAsync(accessToken);

            // Store user information in the request context
            context.Items["User"] = user;
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
