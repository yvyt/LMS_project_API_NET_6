using Microsoft.AspNetCore.Identity;
using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        Task<UserManagerRespone> RegisterUserAsync(RegisterUser user);
        Task<UserManagerRespone> LoginAsync(LoginUser user);
    }
    
    
}
