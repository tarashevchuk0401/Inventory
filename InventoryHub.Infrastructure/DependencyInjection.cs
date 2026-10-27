using InventoryHub.Application.Repositories;
using InventoryHub.Application.Services.Hasher;
using InventoryHub.Application.Services.JWT;
using InventoryHub.Infrastructure.Repositories.InMemory;
using InventoryHub.Infrastructure.Services.Hasher;
using InventoryHub.Infrastructure.Services.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserInMemoryRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}
