using MailService.Models;
using MailService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using UserService.Data;
using UserService.Model;

namespace UserService.Service
{
    public class UserServices : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _config;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IMailServices _mailService;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserServices(UserManager<IdentityUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager
            , IMailServices mailServices, SignInManager<IdentityUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _mailService = mailServices;
            _httpContextAccessor = contextAccessor;
            _signInManager=signInManager;
        }

        public async Task<UserManagerRespone> LoginAsync(LoginUser user)
        {
            if (user == null)
            {
                throw new NullReferenceException("Login is null");
            }
            var userLogin = await _userManager.FindByEmailAsync(user.Email);
            if (userLogin.TwoFactorEnabled)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(userLogin, user.Password, false, true);
                var tokenLogin = await _userManager.GenerateTwoFactorTokenAsync(userLogin, "Email");
                var message = new Message(new string[] { user.Email! }, "OTP Confrimation", tokenLogin);
                _mailService.SendEmail(message);
                return new UserManagerRespone
                {
                    Message=$"An OTP email has been already sent to your email {user.Email}",
                    IsSuccess=true,
                };
            }
            if (userLogin != null && await _userManager.CheckPasswordAsync(userLogin, user.Password))
            {
                var authClaim = new List<Claim>
                {
                   new Claim("Email",user.Email),
                   new Claim(ClaimTypes.NameIdentifier,userLogin.Id),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
                var userRole = await _userManager.GetRolesAsync(userLogin);
                foreach (var role in userRole)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = CreateToken(authClaim);
                return new UserManagerRespone
                {
                    Message = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireDate = token.ValidTo
                };
            }
            return new UserManagerRespone
            {
                Message = "Unauthorized",
                IsSuccess = false
            };
            
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaim)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                    issuer: _config["AuthSettings:Issuer"],
                    audience: _config["AuthSettings:Audience"],
                    claims: authClaim,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public async Task<UserManagerRespone> RegisterUserAsync(RegisterUser user, string role)
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
                TwoFactorEnabled=true
            };
            var checkRole = await _roleManager.RoleExistsAsync(role);
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
                // add token to vetify mail
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var encodedToken = HttpUtility.UrlEncode(token);

                var request = _httpContextAccessor.HttpContext?.Request;

                if (request != null)
                {
                    var baseUrl = $"{request.Scheme}://{request.Host}";

                    var confirmationLink = $"{baseUrl}/User/ConfirmEmail?token={encodedToken}&email={identityUser.Email}";
                    var message = new Message(new string[] { identityUser.Email! }, "Confirmation email link", confirmationLink!);
                    _mailService.SendEmail(message);
                }
                return new UserManagerRespone
                {
                    Message = $"User created & Email Sent to {user.Email} SuccessFully",
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

        public async Task<IdentityUser> SendMailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;

        }
        public async Task<UserManagerRespone> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var decodedToken = HttpUtility.UrlDecode(token);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return new UserManagerRespone
                {
                    Message = "Email verified successfully",
                    IsSuccess = true,
                };
            }
            return new UserManagerRespone
            {
                Message = "Unthorized",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)

            };

        }
    }
}