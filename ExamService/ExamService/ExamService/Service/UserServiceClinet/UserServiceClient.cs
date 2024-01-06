using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamService.Service.UserServiceClinet
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
            var userServiceUrl = $"https://localhost:44357/User/UserByToken?token={accessToken}";
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
    }
}
