using InventoryHub.Application.Dto;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Infrastructure.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
}