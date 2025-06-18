
using Application.Dtos;
using Application.RepositoryContracts;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task DeleteUserByUserIdAsync(long userId, string userRoleName)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> GetUserByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserRoleAsync(long userId, long userRoleId, string userRoleName)
    {
        throw new NotImplementedException();
    }
}
