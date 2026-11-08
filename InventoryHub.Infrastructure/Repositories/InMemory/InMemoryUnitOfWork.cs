using InventoryHub.Application.Repositories;

namespace InventoryHub.Infrastructure.Repositories.InMemory;

public class InMemoryUnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync() => Task.CompletedTask;
}
