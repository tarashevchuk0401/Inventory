using InventoryHub.Application.Repositories;
using InventoryHub.Application.Services.Hasher;
using InventoryHub.Application.Services.JWT;
using InventoryHub.Infrastructure.Persistance;
using InventoryHub.Infrastructure.Repositories.EfCore;
using InventoryHub.Infrastructure.Repositories.InMemory;
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

        var useDatabase = configuration.GetValue<bool>("Persistence:UseDatabase");

        if (useDatabase)
        {
            services.AddScoped<IProductRepository, ProductEfRepository>();
            services.AddScoped<IUserRepository, UserEfRepository>();
            services.AddScoped<ISupplierRepository, SupplierEfRepository>();
            services.AddScoped<ICustomerRepository, CustomerEfRepository>();
            services.AddScoped<IOrderRepository, OrderEfRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemEfRepository>();
        }
        else
        {
            services.AddSingleton<IProductRepository, ProductInMemoryRepository>();
            services.AddSingleton<IUserRepository, UserInMemoryRepository>();
            services.AddSingleton<ISupplierRepository, SupplierInMemoryRepository>();
            services.AddSingleton<ICustomerRepository, CustomerInMemoryRepository>();
            services.AddSingleton<IOrderRepository, OrderInMemoryRepository>();
            services.AddSingleton<IOrderItemRepository, OrderItemInMemoryRepository>();
        }

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}