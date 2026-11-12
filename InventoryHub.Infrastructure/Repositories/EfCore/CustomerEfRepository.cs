using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class CustomerEfRepository(ApplicationDbContext context) : ICustomerRepository
{
    public async Task<List<Customer>> GetAllAsync()
    {
        return await context.Customers.ToListAsync();
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
