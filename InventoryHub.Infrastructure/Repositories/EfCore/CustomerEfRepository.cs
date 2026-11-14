using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace InventoryHub.Infrastructure.Repositories.EfCore;

class CustomerEfRepository(ApplicationDbContext context) : ICustomerRepository
{
    public async Task<PaginationResponseDto<Customer>> GetAllAsync(PaginationRequestDto request)
    {
        var skip = (request.Page - 1) * request.ItemsOnPage;
        var customers = await context.Customers
            .Skip(skip)
            .Take(request.ItemsOnPage)
            .ToListAsync();
        var total = await context.Customers.CountAsync();

        return new PaginationResponseDto<Customer>
        {
            Items = customers,
            Page = request.Page,
            Total = total,
        };
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await context.Customers.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(Customer customer)
    {
        context.Customers.Add(customer);
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        return await context.Customers.AnyAsync(c => c.Id == id);
    }

    public async Task<Customer?> GetWithOrdersIdAsync(Guid id)
    {
        return await context.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
