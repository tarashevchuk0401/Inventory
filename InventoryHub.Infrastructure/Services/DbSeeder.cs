using InventoryHub.Domain.Entities;
using InventoryHub.Domain.Enums;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        await DbSeeder.SeedUsers(db);
        await DbSeeder.SeedSuppliers(db);
        await DbSeeder.SeedCustomers(db);
        await DbSeeder.SeedProducts(db);
        await DbSeeder.SeedOrder(db);
        await DbSeeder.SeedOrderItems(db);
    }

    private static async Task SeedUsers(ApplicationDbContext db)
    {
        if (await db.Users.AnyAsync())
            return;

        db.Users.AddRange(
            new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"), FirstName = "User", LastName = "User",
                Email = "user@example.com", HashedPassword = "string", Role = "Admin"
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedSuppliers(ApplicationDbContext db)
    {
        if (await db.Suppliers.AnyAsync())
            return;

        db.Suppliers.AddRange(
            new Supplier
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Apple",
                Email = "apple@apple.com",
            },
            new Supplier
            {
                Id = new Guid("11111111-1111-1111-1111-111111111112"), Name = "Samsung",
                Email = "contact@samsung.com",
            },
            new Supplier
            {
                Id = new Guid("11111111-1111-1111-1111-111111111113"), Name = "Sony",
                Email = "contact@sony.com",
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedProducts(ApplicationDbContext db)
    {
        if (await db.Products.AnyAsync())
            return;

        var appleId = new Guid("11111111-1111-1111-1111-111111111111");
        var samsungId = new Guid("11111111-1111-1111-1111-111111111112");
        var sonyId = new Guid("11111111-1111-1111-1111-111111111113");

        db.Products.AddRange(
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222221"), Name = "Iphone 18",
                Price = 750, StockQuantity = 100, SupplierId = appleId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "Iphone 18 Pro",
                Price = 999, StockQuantity = 80, SupplierId = appleId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222223"), Name = "MacBook Air",
                Price = 1199, StockQuantity = 40, SupplierId = appleId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222224"), Name = "AirPods Pro",
                Price = 249, StockQuantity = 150, SupplierId = appleId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222225"), Name = "Galaxy S25",
                Price = 820, StockQuantity = 90, SupplierId = samsungId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222226"), Name = "Galaxy Z Fold 6",
                Price = 1599, StockQuantity = 30, SupplierId = samsungId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222227"), Name = "Galaxy Tab S10",
                Price = 650, StockQuantity = 60, SupplierId = samsungId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222228"), Name = "Galaxy Buds Pro",
                Price = 179, StockQuantity = 120, SupplierId = samsungId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-222222222229"), Name = "PlayStation 5",
                Price = 499, StockQuantity = 70, SupplierId = sonyId,
            },
            new Product
            {
                Id = new Guid("22222222-2222-2222-2222-22222222222a"), Name = "Sony WH-1000XM6",
                Price = 349, StockQuantity = 110, SupplierId = sonyId,
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedCustomers(ApplicationDbContext db)
    {
        if (await db.Customers.AnyAsync())
            return;

        db.Customers.AddRange(
            new Customer
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"), FirstName = "User", LastName = "User",
                Email = "user@example.com",
            },
            new Customer
            {
                Id = new Guid("33333333-3333-3333-3333-333333333331"), FirstName = "John", LastName = "Doe",
                Email = "john.doe@example.com",
            },
            new Customer
            {
                Id = new Guid("33333333-3333-3333-3333-333333333332"), FirstName = "Jane", LastName = "Smith",
                Email = "jane.smith@example.com",
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedOrder(ApplicationDbContext db)
    {
        if (await db.Orders.AnyAsync())
            return;

        db.Orders.AddRange(
            new Order
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                CustomerId = new Guid("11111111-1111-1111-1111-111111111111"),
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedOrderItems(ApplicationDbContext db)
    {
        if (await db.OrderItems.AnyAsync())
            return;

        db.OrderItems.AddRange(
            new OrderItem
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                OrderId = new Guid("11111111-1111-1111-1111-111111111111"),
                ProductId = new Guid("22222222-2222-2222-2222-222222222221"),
                Quantity = 1,
                UnitPriceInOrder = 750,
            }
        );

        await db.SaveChangesAsync();
    }
}
