﻿using FluentCMS.Identity;
using FluentCMS.Services;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IPermissionManager, PermissionManager>();

        services.AddScoped<IUserTokenProvider, JwtUserTokenProvider>();

        AddIdentity(services);

        RegisterServices(services);

        return services;
    }

    private static IdentityBuilder AddIdentity(IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration("JwtOptions");

        var builder = services.AddIdentityCore<User>();

        builder
            .AddUserStore<UserStore>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(ServiceConstants.PASSWORD_RESET_TOKEN_PROVIDER);

        return builder;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        var serviceTypes = typeof(ServiceExtensions).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service") && t.GetInterfaces().Contains(typeof(IAutoRegisterService)))
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var interfaceType = serviceType.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith(serviceType.Name))
                ?? throw new InvalidOperationException($"Interface for service '{serviceType.Name}' not found.");

            services.AddScoped(interfaceType, serviceType);
        }
    }

}
