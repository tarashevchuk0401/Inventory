using InventoryHub.Application.Dto.Customer;

namespace InventoryHub.Application.Services;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetById(Guid id);

    Task<CustomerDto> AddAsync(CreateCustomerDto dto);
}
