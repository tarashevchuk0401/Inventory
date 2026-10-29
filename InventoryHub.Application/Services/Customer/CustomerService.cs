using InventoryHub.Application.Dto.Customer;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var all = await customerRepository.GetAllAsync();

        return all
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            })
            .ToList();
    }

    public async Task<CustomerDto> GetById(Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            throw new NotFoundException("There no customer with such id.");
        }

        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email
        };
    }

    public async Task<CustomerDto> AddAsync(CreateCustomerDto dto)
    {
        var newCustomer = Customer.Create(dto.FirstName, dto.LastName, dto.Email);
        await customerRepository.AddAsync(newCustomer);

        return new CustomerDto
        {
            Id = newCustomer.Id,
            FirstName = newCustomer.FirstName,
            LastName = newCustomer.LastName,
            Email = newCustomer.Email
        };
    }
}
