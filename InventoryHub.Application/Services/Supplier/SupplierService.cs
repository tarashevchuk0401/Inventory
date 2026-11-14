using InventoryHub.Application.Dto.Supplier;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class SupplierService(
    ISupplierRepository supplierRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : ISupplierService
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

    public async Task DeleteSupplier(Guid id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id);
        if (supplier == null)
        {
            throw new NotFoundException(" There are no supplier");
        }

        var products = await productRepository.GetBySupplier(id);
        if (products.Count > 0)
        {
            throw new ConflictException("This supplier have product!");
        }

        await supplierRepository.DeleteAsync(supplier);
        await unitOfWork.SaveChangesAsync();
    }
}
