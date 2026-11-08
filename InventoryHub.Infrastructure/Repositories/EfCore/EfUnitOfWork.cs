using InventoryHub.Application.Repositories;
using InventoryHub.Infrastructure.Persistance;

namespace InventoryHub.Infrastructure.Repositories.EfCore;

class EfUnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
