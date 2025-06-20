
using Application.Dtos;
using Application.RepositoryContracts;
using Core.Errors;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task DeleteUserByUserIdAsync(long userId, string userRoleName)
    {
        if (userRoleName == "SuperAdmin")
        {
            await userRepository.DeleteUserById(userId);
        }
        else if (userRoleName == "Admin")
        {
            var user = await userRepository.SelectUserByIdAsync(userId);

            if (user.Role.Name == "User" && user.UserId == userId)
            {
                await userRepository.DeleteUserById(userId);
            }
            else
            {
                throw new NotAllowedException("Admin can not delete admin");
            }
        }
        else
        {
            throw new ForbiddenException("Access forbidden to users");
        }
    }

    public async Task<UserGetDto> GetUserByUserIdAsync(long userId)
    {

        var user = await userRepository.SelectUserByIdAsync(userId);
        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Role = user.Role.Name,
        };

        return userGetDto;
    }

    public async Task UpdateUserRoleAsync(long userId, long userRoleId, string userRoleName)
    {
        await(userRoleName == "SuperAdmin"
            ? userRepository.UpdateUserRoleAsync(userId, userRoleId)
            : throw new NotAllowedException("Updating is not allowed for Users or Admin"));
    }
}
