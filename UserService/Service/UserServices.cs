using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Model;

namespace UserService.Service
{
    public class UserServices : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _config;
        private RoleManager<IdentityRole> _roleManager;
        public UserServices(UserManager<IdentityUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<UserManagerRespone> LoginAsync(LoginUser user)
        {
            if(user == null)
            {
                throw new NullReferenceException("Login is null");
            }
            var userLogin= await _userManager.FindByEmailAsync(user.Email);
            if (userLogin == null)
            {
                return new UserManagerRespone
                {
                    Message = "There is no user with that email address",
                    IsSuccess = false
                };
            }
            var result = await _userManager.CheckPasswordAsync(userLogin, user.Password);
            if (!result)
            {
                return new UserManagerRespone
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }
            var Claims = new[]
            {
               new Claim("Email",user.Email),
               new Claim(ClaimTypes.NameIdentifier,userLogin.Id),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                    issuer: _config["AuthSettings:Issuer"],
                    audience: _config["AuthSettings:Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            string accesstoken= new JwtSecurityTokenHandler().WriteToken(token);
            return new UserManagerRespone
            {
                Message = accesstoken,
                IsSuccess = true,
                ExpireDate= token.ValidTo,
            };
        }

        public async Task<UserManagerRespone> RegisterUserAsync(RegisterUser user,string role)
        {
            if (user == null)
            {
                throw new NullReferenceException("Register model is null");
            }
            if (user.Password != user.ConfirmPassword)
            {
                return new UserManagerRespone
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }
            var identityUser = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email,
            };
            var checkRole= await _roleManager.RoleExistsAsync(role);
            if (checkRole)
            {
                var result = await _userManager.CreateAsync(identityUser, user.Password);
                
                if (!result.Succeeded)
                {
                    return new UserManagerRespone
                    {
                        Message = "User did not create",
                        IsSuccess = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }
                // add to role
                await _userManager.AddToRoleAsync(identityUser, role);
                return new UserManagerRespone
                {
                    Message = "User created successful",
                    IsSuccess = true,
                };

            }
            else
            {
                return new UserManagerRespone
                {
                    Message = "Role doesn't exitst",
                    IsSuccess = false,
                };
            }
            
            
        }
    }
}