using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;
using InventoryHub.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


class SupplierEfRepository(ApplicationDbContext context) : ISupplierRepository
{
    public async Task<List<Supplier>> GetAllAsync()
    {
        return await context.Suppliers.ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(Guid id)
    {
        return await context.Suppliers.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
    }
}