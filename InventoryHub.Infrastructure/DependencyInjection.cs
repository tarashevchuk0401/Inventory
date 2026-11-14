using InventoryHub.Application.Repositories;
using InventoryHub.Application.Services.Hasher;
using InventoryHub.Application.Services.JWT;
using InventoryHub.Infrastructure.Persistance;
using InventoryHub.Infrastructure.Repositories.EfCore;
using InventoryHub.Infrastructure.Services.Hasher;
using InventoryHub.Infrastructure.Services.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IProductRepository, ProductEfRepository>();
        services.AddScoped<IUserRepository, UserEfRepository>();
        services.AddScoped<ISupplierRepository, SupplierEfRepository>();
        services.AddScoped<ICustomerRepository, CustomerEfRepository>();
        services.AddScoped<IOrderRepository, OrderEfRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemEfRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}
