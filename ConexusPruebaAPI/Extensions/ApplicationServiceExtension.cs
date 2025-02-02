using App.UnitOfWork;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ConexusPruebaAPI.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
