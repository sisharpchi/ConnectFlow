
using Application.Helpers;
using Server.ActionHelpers;
using Server.Configurations;
using Server.Endpoints;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.ConfigureDatabase();
            builder.ConfigureDependencies();
            builder.ConfigurationJwtAuth();

            var app = builder.Build();
            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapAuthEndpoints();
            app.MapContactEndpoints();
            app.MapAdminEndpoints();

            app.Run();
        }
    }
}
