﻿using CourseService.Model;

namespace CourseService.Service.UserServiceClinet
{
    public interface IUserServiceClient
    {
        Task<UserDTO> GetUserDetailsAsync(string accessToken);

    }
}
