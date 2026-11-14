using InventoryHub.Application.Dto.Customer;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class CustomerService(
    ICustomerRepository customerRepository
) : ICustomerService
{
    public async Task<PaginationResponseDto<CustomerDto>> GetAll(PaginationRequestDto request)
    {
        var response = await customerRepository.GetAllAsync(request);

        return new PaginationResponseDto<CustomerDto>
        {
            Items = response.Items.Select(o => new CustomerDto
            {
                Id = o.Id,
                Email = o.Email,
                FirstName = o.FirstName,
                LastName = o.LastName
            }).ToList(),
            Page = response.Page,
            Total = response.Total
        };
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


    public async Task<decimal> GetRevenueByCustomer(Guid id)
    {
        var customer = await customerRepository.GetWithOrdersIdAsync(id);
        if (customer == null)
        {
            throw new NotFoundException(" There no customer with such id");
        }

        return customer.Orders.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPriceInOrder));
    }
}
