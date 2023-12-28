using Microsoft.AspNetCore.Identity;
using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        Task<UserManagerRespone> RegisterUserAsync(RegisterUser user,string role);
        Task<UserManagerRespone> LoginAsync(LoginUser user);
        UserManagerRespone SendMailAsync();
    }
    
    
}
