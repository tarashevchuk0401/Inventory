using InventoryHub.Application.Dto.Customer;

namespace InventoryHub.Application.Services;

public interface ICustomerService
{
    Task<PaginationResponseDto<CustomerDto>> GetAll(PaginationRequestDto request);
    Task<CustomerDto> GetById(Guid id);
    Task<CustomerDto> AddAsync(CreateCustomerDto dto);
    Task<decimal> GetRevenueByCustomer(Guid id);
}
