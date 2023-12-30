using Microsoft.AspNetCore.Identity;
using UserService.Model;

namespace UserService.Service
{
    public interface IUserService
    {
        Task<UserManagerRespone> RegisterUserAsync(RegisterUser user,string role);
        Task<UserManagerRespone> LoginAsync(LoginUser user);
        Task<IdentityUser> SendMailAsync( string email);
        Task<UserManagerRespone> ConfirmEmail(string email, string token);
        Task<UserManagerRespone> LoginWithOTP(string otp, string email);
        Task<List<IdentityUser>> GetAll();
        Task<UserManagerRespone> ForgotPassword(string email);
        UserManagerRespone GetResetPassword(string token, string email);
        Task<UserManagerRespone> ResetPassword(ResetPassword model);
        Task<IdentityUser> GetUser(string id);
        Task<UserManagerRespone> LogOut();
    }
    
    
}
