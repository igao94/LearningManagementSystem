using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Constants;

namespace Persistence.Data;

public class Seed
{
    public static async Task SeedDataAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        await CreateRolesAsync(roleManager);

        await CreateUsersAsync(userManager);

        await CreateAdminAsync(userManager);
    }

    private static async Task CreateAdminAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                Id = "admin-id",
                Email = "admin@test.com",
                UserName = "admin",
                FirstName = "Admin",
                LastName = "Admin"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");

            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
    }

    private static async Task CreateUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            List<User> users =
            [
                new ()
                {
                    Id = "john-id",
                    Email = "john@test.com",
                    UserName = "john",
                    FirstName = "John",
                    LastName = "Doe"
                },

                new ()
                {
                    Id = "jane-id",
                    Email = "jane@test.com",
                    UserName = "jane",
                    FirstName = "Jane",
                    LastName = "Smith"
                },

                new ()
                {
                    Id = "michael-id",
                    Email = "michael@test.com",
                    UserName = "michael",
                    FirstName = "Michael",
                    LastName = "Johnson"
                },

                new ()
                {
                    Id = "emily-id",
                    Email = "emily@test.com",
                    UserName = "emily",
                    FirstName = "Emily",
                    LastName = "Davis"
                },

                new ()
                {
                    Id = "david-id",
                    Email = "david@test.com",
                    UserName = "david",
                    FirstName = "David",
                    LastName = "Wilson"
                }
            ];

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                await userManager.AddToRoleAsync(user, UserRoles.Student);
            }
        }
    }

    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            List<IdentityRole> roles =
            [
                new() { Name = UserRoles.Student, NormalizedName = UserRoles.Student.ToUpper() },

                new() { Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() }
            ];

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}