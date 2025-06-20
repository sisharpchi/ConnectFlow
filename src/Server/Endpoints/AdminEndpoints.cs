using Application.ServiceContracts;
using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Server.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/admin")
            .RequireAuthorization()
            .WithTags("Admin endpoints");

        userGroup.MapGet("/getUsersByRole",
            [Authorize(Roles = "Admin, SuperAdmin, Super Admin")]
        async (string role, IRoleService _userRoleService) =>
            {
                var users = await _userRoleService.GetAllUsersByRoleNameAsync(role);
                return Results.Ok(users);
            })
        .WithName("GetUsersByRole");

        userGroup.MapDelete("/delete", [Authorize(Roles = "Admin, SuperAdmin")]
        async (long userId, HttpContext httpContext, IUserService userService) =>
        {
            var userRoleName = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            await userService.DeleteUserByUserIdAsync(userId, userRoleName);
            return Results.Ok();
        })
        .WithName("DeleteUserByRole");

        userGroup.MapPatch("/changeRole", [Authorize(Roles = "SuperAdmin")]
        async (long userId, long userRoleId, IUserService userService, HttpContext httpContext) =>
        {
            var userRoleName = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRoleName == null)
                throw new NotAllowedException("Invalid token");

            await userService.UpdateUserRoleAsync(userId, userRoleId, userRoleName);
            return Results.Ok();
        })
        .WithName("UpdateUserRole");

        // Barcha rollarni olish
        userGroup.MapGet("/getRoles", [Authorize(Roles = "Admin,SuperAdmin")]
        async (IRoleService _roleService) =>
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Results.Ok(roles);
        })
        .WithName("GetAllRoles");

        //// Userni role'i bilan birga olish
        //userGroup.MapGet("/getUserWithRole", [Authorize(Roles = "Admin,SuperAdmin")]
        //async (long userId, IUserService _userService) =>
        //{
        //    var user = await _userService.GetUserWithRoleByIdAsync(userId);
        //    return Results.Ok(user);
        //})
        //.WithName("GetUserWithRole");

    }
}
