using InventoryHub.Application.Dto.Supplier;

namespace InventoryHub.Application.Services;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetAllAsync();
    Task<SupplierDto> GetById(Guid id);

    Task<SupplierDto> AddAsync (CreateSupplierDto dto);
}
