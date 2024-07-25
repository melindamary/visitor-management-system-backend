﻿using VMS.Models;
using VMS.Models.DTO;

namespace VMS.Repository.IRepository
{
    public interface IUserRoleRepository
    {
        Task<UserRole> GetUserRoleByUserIdAsync(int userId);
        Task AddUserRoleAsync(UserRole userRole);
        Task<UserRole> AddVisitorDeviceAsync(AddUserRoleDTO addUserRoleDto);
    }
}
