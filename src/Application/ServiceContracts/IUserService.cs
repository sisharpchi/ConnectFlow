using Application.Dtos;

namespace Application.ServiceContracts;

public interface IUserService
{
    Task DeleteUserByUserIdAsync(long userId, string userRoleName);
    Task UpdateUserRoleAsync(long userId, long userRoleId, string userRoleName);
    Task<UserGetDto> GetUserByUserIdAsync(long userId);
}
