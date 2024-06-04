using Base.Dtos;
using Base.Entities;

namespace Base.Services.Interfaces;

public interface IUserService
{
    Task RegisterAdminUser();
    Task<User> Create(UserAddDto dto);
    Task Update(User user, UserUpdateDto dto);
    Task UpdatePassword(User user, string oldPass, string newPass);
}