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
            }
        );

        await db.SaveChangesAsync();
    }

    private static async Task SeedProducts(ApplicationDbContext db)
    {
        if (await db.Products.AnyAsync())
            return;

        db.Products.AddRange(
            new Product
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Iphone 18",
                Price = 750, StockQuantity = 100, SupplierId = new Guid("11111111-1111-1111-1111-111111111111"),
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
                ProductId = new Guid("11111111-1111-1111-1111-111111111111"),
                Quantity = 1,
                UnitPriceInOrder = 750,
            }
        );

        await db.SaveChangesAsync();
    }
}