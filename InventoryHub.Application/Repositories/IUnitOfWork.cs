namespace InventoryHub.Application.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
