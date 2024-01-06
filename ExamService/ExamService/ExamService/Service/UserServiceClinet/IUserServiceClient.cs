using ExamService.Model;

namespace ExamService.Service.UserServiceClinet
{
    public interface IUserServiceClient
    {
        Task<UserDTO> GetUserDetailsAsync(string accessToken);

    }
}
