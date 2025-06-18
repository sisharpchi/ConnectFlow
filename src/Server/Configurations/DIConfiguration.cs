using Application.RepositoryContracts;
using Application.ServiceContracts;
using Application.ServiceContracts.ServiceImplementations;
using Infrastructure.Persistence.Repositories;

namespace Server.Configurations;

public static class DIConfiguration
{
    public static void ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}