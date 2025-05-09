﻿using Application.Accounts.Commands.Register;
using Application.Accounts.Validators;
using Application.Core;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(RegisterCommand).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
