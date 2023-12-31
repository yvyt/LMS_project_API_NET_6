using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using UserService.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CourseService.Service.UserServiceClinet
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _config;

        public UserServiceClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<UserDTO> GetUserDetailsAsync(string accessToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));


            var decodedToken = DecodeJwtToken(accessToken, key);

            // Access decoded token claims
            var userId = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var email = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
            var role = decodedToken?.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            var userServiceUrl = $"https://localhost:44357/User/UserById?id={userId}";

            _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

            var response = await _httpClient.GetAsync(userServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var jsonContent = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }

            // Handle errors or return null if user not found
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
