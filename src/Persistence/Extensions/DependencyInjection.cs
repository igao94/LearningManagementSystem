using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            var useInMemoryDatabase = config.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                opt.UseInMemoryDatabase("InMemoryDatabase");
            }
            else
            {
                opt.UseSqlServer(config.GetConnectionString("SqlServer"));
            }
        });

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
