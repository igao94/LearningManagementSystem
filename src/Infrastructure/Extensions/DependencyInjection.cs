using Application.Interfaces;
using Infrastructure.Security;
using Infrastructure.Services;
using Infrastructure.Services.EmailServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:TokenKey"]!));

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = key
                };
            });

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddHttpContextAccessor();

        services
            .AddFluentEmail(config["Email:SenderEmail"], config["Email:Sender"])
            .AddSmtpSender(config["Email:Host"], config.GetValue<int>("Email:Port"));

        services.AddScoped<IEmailSender, EmailSender>();

        services.AddScoped<IEmailVerificationLinkFactory, EmailVerificationLinkFactory>();

        return services;
    }
}
