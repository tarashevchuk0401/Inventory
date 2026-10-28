using InventoryHub.Application.Dto.Supplier;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
{
    public async Task<List<SupplierDto>> GetAllAsync()
    {
        var all = await supplierRepository.GetAllAsync();

        return all
            .Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            })
            .ToList();
    }

    public async Task<SupplierDto> GetById(Guid id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id);
        if (supplier == null)
        {
            throw new NotFoundException("There no supplier with such id.");
        }

        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Email = supplier.Name
        };
    }

    public async Task<SupplierDto> AddAsync(CreateSupplierDto dto)
    {
        var newSupplier = Supplier.Create(dto.Email, dto.Name);
        await supplierRepository.AddAsync(newSupplier);

        return new SupplierDto
        {
            Id = newSupplier.Id,
            Email = dto.Email,
            Name = newSupplier.Name
        };
    }
}
