using PrivateFileService.Model;

namespace PrivateFileService.Service.UserServiceClinet
{
    public interface IUserServiceClient
    {
        Task<UserDTO> GetUserDetailsAsync(string accessToken);

    }
}
