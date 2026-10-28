using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class SupplierInMemoryRepository : ISupplierRepository
{
    private readonly List<Supplier> _suppliers = new()
    {
        Supplier.Create("acme@example.com", "Acme Supplies"),
        Supplier.Create("globex@example.com", "Globex Corp")
    };

    public Task<List<Supplier>> GetAllAsync()
    {
        return Task.FromResult(_suppliers);
    }

    public Task<Supplier?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_suppliers.FirstOrDefault(s => s.Id == id));
    }

    public Task AddAsync(Supplier supplier)
    {
        _suppliers.Add(supplier);
        return Task.CompletedTask;
    }
}
