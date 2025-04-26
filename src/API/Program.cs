using API.Extensions;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using IServiceScope scope = await SeedDatabaseAsync(app);

app.Run();

static async Task<IServiceScope> SeedDatabaseAsync(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        var userManager = services.GetRequiredService<UserManager<User>>();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var useInMemoryDatabase = services.GetRequiredService<IConfiguration>()
            .GetValue<bool>("UseInMemoryDatabase");

        if (!useInMemoryDatabase) await context.Database.MigrateAsync();

        await Seed.SeedDataAsync(userManager, roleManager);
    }

    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex.Message);
    }

    return scope;
}