using InventoryHub.Application.Dto.Order;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IOrderItemRepository orderItemRepository,
    IProductRepository productRepository) : IOrderService
{
    public async Task<List<OrderDto>> GetAllAsync()
    {
        var all = await orderRepository.GetAllAsync();

        return all
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status
            })
            .ToList();
    }

    public async Task<OrderDto> GetById(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            throw new NotFoundException("There no order with such id.");
        }

        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            Status = order.Status
        };
    }

    public async Task<OrderDto> AddAsync(CreateOrderDto dto)
    {
        var isCustomerExist = await customerRepository.IsExistAsync(dto.CustomerId);
        if (!isCustomerExist)
        {
            throw new NotFoundException($"There are no customer with id {dto.CustomerId}");
        }

        var newOrder = Order.Create(dto.CustomerId);
        await orderRepository.AddAsync(newOrder);

        var orderItems = dto.OrderItems;
        foreach (var item in orderItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"There are no product with id {item.ProductId}");
            }

            var newItem = OrderItem.Create(newOrder.Id, item.ProductId, item.Quantity, product.Price);
            await orderItemRepository.AddAsync(newItem);
        }

        return new OrderDto
        {
            Id = newOrder.Id,
            CustomerId = newOrder.CustomerId,
            OrderDate = newOrder.OrderDate,
            Status = newOrder.Status
        };
    }
}
