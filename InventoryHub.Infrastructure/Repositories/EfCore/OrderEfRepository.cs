using System.ComponentModel;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class OrderEfRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<PaginationResponseDto<Order>> GetAllAsync(PaginationRequestDto<OrderSortBy> request)
    {
        IQueryable<Order> query = context.Orders;
        query = (request.SortBy, request.Direction) switch
        {
            (OrderSortBy.CustomerId, ListSortDirection.Descending) => query.OrderByDescending(o => o.CustomerId),
            (OrderSortBy.CustomerId, _) => query.OrderBy(o => o.CustomerId),
            (OrderSortBy.OrderDate, ListSortDirection.Descending) => query.OrderByDescending(o => o.OrderDate),
            _ => query.OrderBy(o => o.OrderDate),
        };
        var skip = (request.Page - 1) * request.ItemsOnPage;
        var totalOrders = await context.Orders.CountAsync();
        var orders = await query
            .Skip(skip)
            .Take(request.ItemsOnPage)
            .ToListAsync();

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
