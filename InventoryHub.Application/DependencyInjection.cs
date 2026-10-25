using InventoryHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
