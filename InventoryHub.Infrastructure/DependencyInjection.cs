using InventoryHub.Application.Repositories;
using InventoryHub.Infrastructure.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserInMemoryRepository>();

        return services;
    }
}
