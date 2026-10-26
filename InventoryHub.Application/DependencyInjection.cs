using InventoryHub.Application.Services;
using InventoryHub.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
