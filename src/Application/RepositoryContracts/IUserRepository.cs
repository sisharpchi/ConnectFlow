using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IUserRepository
{
    Task<long> InsertUserAsync(User user);
    Task<User> SelectUserByIdAsync(long userId);
    Task<User> SelectUserByUserNameAsync(string userName);
    Task UpdateUserRoleAsync(long userId, long userRoleId);
    Task DeleteUserById(long userId);
}