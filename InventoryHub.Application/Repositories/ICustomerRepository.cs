using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Repositories;

public interface ICustomerRepository
{
    Task<PaginationResponseDto<Customer>> GetAllAsync(PaginationRequestDto request);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetWithOrdersIdAsync(Guid id);
    Task AddAsync(Customer customer);
    Task<bool> IsExistAsync(Guid id);
}
