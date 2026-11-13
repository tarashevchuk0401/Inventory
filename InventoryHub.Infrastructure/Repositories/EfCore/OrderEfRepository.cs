using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class OrderEfRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<PaginationResponseDto<Order>> GetAllAsync(PaginationRequestDto request)
    {
        var skip = (request.Page - 1) * request.ItemsOnPage;
        var orders = await context.Orders
            .OrderBy(o => o.OrderDate)
            .Skip(skip)
            .Take(request.ItemsOnPage)
            .ToListAsync();

        var totalOrders = await context.Orders.CountAsync();

        return new PaginationResponseDto<Order>
        {
            Items = orders,
            Page = request.Page,
            Total = totalOrders,
        };
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    // Tracks the order only — caller must call IUnitOfWork.SaveChangesAsync() to persist.
    public Task AddAsync(Order order)
    {
        context.Orders.Add(order);
        return Task.CompletedTask;
    }
}
