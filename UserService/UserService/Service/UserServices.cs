using MailService.Models;
using MailService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
            _signInManager = signInManager;
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
                    Message = $"An OTP email has been already sent to your email {user.Email}",
                    IsSuccess = true,
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
                    if (role == "Leadership")
                    {
                        authClaim.Add(new Claim("Permission", "Create:Course"));
                        authClaim.Add(new Claim("Permission", "Edit:Course"));
                        authClaim.Add(new Claim("Permission", "Delete:Course"));
                        authClaim.Add(new Claim("Permission", "View:Course"));

                        authClaim.Add(new Claim("Permission", "Create:Class"));
                        authClaim.Add(new Claim("Permission", "Edit:Class"));
                        authClaim.Add(new Claim("Permission", "Delete:Class"));
                        authClaim.Add(new Claim("Permission", "View:Class"));

                        authClaim.Add(new Claim("Permission", "View:Topic"));
                        authClaim.Add(new Claim("Permission", "View:Lesson"));
                        authClaim.Add(new Claim("Permission", "View:ExamQuestion"));
                        authClaim.Add(new Claim("Permission", "View:Question"));
                        authClaim.Add(new Claim("Permission", "View:Answer"));

                        authClaim.Add(new Claim("Permission", "Create:Resource"));
                        authClaim.Add(new Claim("Permission", "Edit:Resource"));
                        authClaim.Add(new Claim("Permission", "Delete:Resource"));
                        authClaim.Add(new Claim("Permission", "View:Resource"));

                        authClaim.Add(new Claim("Permission", "Create:Private File"));
                        authClaim.Add(new Claim("Permission", "Edit:Private File"));
                        authClaim.Add(new Claim("Permission", "Delete:Private File"));
                        authClaim.Add(new Claim("Permission", "View:Private File"));

                        authClaim.Add(new Claim("Permission", "Create:User"));
                        authClaim.Add(new Claim("Permission", "Edit:User"));
                        authClaim.Add(new Claim("Permission", "Delete:User"));
                        authClaim.Add(new Claim("Permission", "View:User"));
                    }
                    if (role == "Teacher")
                    {
                        authClaim.Add(new Claim("Permission", "View:Class"));
                        authClaim.Add(new Claim("Permission", "View:Course"));

                        authClaim.Add(new Claim("Permission", "Create:Topic"));
                        authClaim.Add(new Claim("Permission", "Edit:Topic"));
                        authClaim.Add(new Claim("Permission", "Delete:Topic"));
                        authClaim.Add(new Claim("Permission", "View:Topic"));

                        authClaim.Add(new Claim("Permission", "Create:Lesson"));
                        authClaim.Add(new Claim("Permission", "Edit:Lesson"));
                        authClaim.Add(new Claim("Permission", "Delete:Lesson"));
                        authClaim.Add(new Claim("Permission", "View:Lesson"));

                        authClaim.Add(new Claim("Permission", "Create:Resource"));
                        authClaim.Add(new Claim("Permission", "Edit:Resource"));
                        authClaim.Add(new Claim("Permission", "Delete:Resource"));
                        authClaim.Add(new Claim("Permission", "View:Resource"));

                        authClaim.Add(new Claim("Permission", "Create:Exam"));
                        authClaim.Add(new Claim("Permission", "Edit:Exam"));
                        authClaim.Add(new Claim("Permission", "Delete:Exam"));
                        authClaim.Add(new Claim("Permission", "View:Exam"));

                        authClaim.Add(new Claim("Permission", "Create:ExamQuestion"));
                        authClaim.Add(new Claim("Permission", "Edit:ExamQuestion"));
                        authClaim.Add(new Claim("Permission", "Delete:ExamQuestion"));
                        authClaim.Add(new Claim("Permission", "View:ExamQuestion"));

                        authClaim.Add(new Claim("Permission", "Create:Question"));
                        authClaim.Add(new Claim("Permission", "Edit:Question"));
                        authClaim.Add(new Claim("Permission", "Delete:Question"));
                        authClaim.Add(new Claim("Permission", "View:Question"));

                        authClaim.Add(new Claim("Permission", "Create:Answer"));
                        authClaim.Add(new Claim("Permission", "Edit:Answer"));
                        authClaim.Add(new Claim("Permission", "Delete:Answer"));
                        authClaim.Add(new Claim("Permission", "View:Answer"));

                        authClaim.Add(new Claim("Permission", "View:User"));

                    }
                    if (role == "Student")
                    {
                        authClaim.Add(new Claim("Permission", "View:Course"));
                        authClaim.Add(new Claim("Permission", "View:Class"));
                        authClaim.Add(new Claim("Permission", "View:User"));
                        authClaim.Add(new Claim("Permission", "View:Exam"));
                        authClaim.Add(new Claim("Permission", "View:Topic"));
                        authClaim.Add(new Claim("Permission", "View:Lesson"));
                    }
                }
                
                var token = CreateToken(authClaim);
                return new UserManagerRespone
                {
                    IsSuccess = true,
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
                UserName = user.Username,
                PhoneNumber = user.Phone,
                TwoFactorEnabled = true
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
        public async Task<UserManagerRespone> LoginWithOTP(string otp, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _signInManager.TwoFactorSignInAsync("Email", otp, false, false);
            if (result.Succeeded)
            {
                if (user != null)
                {
                    var authClaim = new List<Claim>
                    {
                       new Claim("Email",user.Email),
                       new Claim(ClaimTypes.NameIdentifier,user.Id),
                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                    };
                    var userRole = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRole)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var token = CreateToken(authClaim);
                    return new UserManagerRespone
                    {
                        IsSuccess = true,
                        Message = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpireDate = token.ValidTo
                    };
                }
                return new UserManagerRespone
                {
                    IsSuccess = false,
                    Message = $"Doesn't exist user with {email}",
                };
            }
            return new UserManagerRespone
            {
                Message = "Unthorized",
                IsSuccess = false,

            };
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var result = new List<UserDTO>();
            foreach(var user in allUsers)
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var firstRole = roleName.FirstOrDefault();

                UserDTO userDTO = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email=user.Email,
                    RoleName=firstRole
                };
                result.Add(userDTO);
            }
            return result;
        }

        public async Task<UserManagerRespone> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var encodedToken = WebUtility.UrlEncode(token);

                var request = _httpContextAccessor.HttpContext?.Request;

                if (request != null)
                {
                    var baseUrl = $"{request.Scheme}://{request.Host}";

                    var link = $"{baseUrl}/User/ResetPassword?token={token}&email={user.Email}";
                    var message = new Message(new string[] { user.Email! }, "Reset Password link", link!);
                    _mailService.SendEmail(message);
                }
                return new UserManagerRespone
                {
                    Message = $"{token}",
                    IsSuccess = true,

                };
            }
            return new UserManagerRespone
            {
                Message = $"Don't exist user with {email}",
                IsSuccess = false,
            };
        }

        public UserManagerRespone GetResetPassword(string token, string email)
        {
            var resetModel = new ResetPassword
            {
                Token = token,
                Email = email
            };
            return new UserManagerRespone
            {
                Message = $"Token:{resetModel.Token}",
                IsSuccess = true
            };
        }

        public async Task<UserManagerRespone> ResetPassword(ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //var decodedToken = HttpUtility.UrlDecode(model.Token);
                //var decodedToken = WebUtility.UrlDecode(model.Token);
                var code = model.Token.Replace(" ", "+");

                var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var r in result.Errors)
                    {
                        return new UserManagerRespone
                        {
                            Message = "Password don't reset",
                            IsSuccess = false,
                            Errors = result.Errors.Select(e => e.Description)
                        };
                    }
                }
                return new UserManagerRespone
                {
                    Message = "Password has been reset",
                    IsSuccess = true,
                };

            }
            return new UserManagerRespone
            {
                Message = $"Don't exist user with {model.Email}",
                IsSuccess = true,
            };
        }

        public async Task<UserDTO> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var firstRole = roleName.FirstOrDefault();

                return new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleName = firstRole,
                    UserName = user.UserName
                };
            }
            return null;
        }

        public async Task<UserManagerRespone> LogOut()
        {
            await _signInManager.SignOutAsync();
            return new UserManagerRespone
            {
                Message = "Log Out successully"
            };

        }
        public async Task<UserDTO> GetUserByToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var decodedToken = DecodeJwtToken(token, key);

            var userId = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var email = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
            var role = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var firstRole = roleName.FirstOrDefault();

                return new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleName = firstRole,
                    UserName=user.UserName
                };
            }
            return null;
        }
        private JwtSecurityToken DecodeJwtToken(string accessToken, SymmetricSecurityKey key)
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
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            return securityToken as JwtSecurityToken;
        }
    }
}