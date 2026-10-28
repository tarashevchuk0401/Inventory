using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class CustomerInMemoryRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new()
    {
        Customer.Create("Carl", "Jones", "carl@example.com"),
        Customer.Create("Dana", "White", "dana@example.com")
    };

    public Task<List<Customer>> GetAllAsync()
    {
        return Task.FromResult(_customers);
    }

    public Task<Customer?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
    }

    public Task AddAsync(Customer customer)
    {
        _customers.Add(customer);
        return Task.CompletedTask;
    }
}
