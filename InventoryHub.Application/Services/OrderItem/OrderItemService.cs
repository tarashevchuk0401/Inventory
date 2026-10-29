using InventoryHub.Application.Dto.OrderItem;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class OrderItemService(IOrderItemRepository orderItemRepository) : IOrderItemService
{
    public async Task<List<OrderItemDto>> GetAllAsync()
    {
        var all = await orderItemRepository.GetAllAsync();

        return all
            .Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPriceInOrder = oi.UnitPriceInOrder
            })
            .ToList();
    }

    public async Task<OrderItemDto> GetById(Guid id)
    {
        var orderItem = await orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
        {
            throw new NotFoundException("There no order item with such id.");
        }

        return new OrderItemDto
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity,
            UnitPriceInOrder = orderItem.UnitPriceInOrder
        };
    }

    public async Task<OrderItemDto> AddAsync(CreateOrderItemDto dto)
    {
        var newOrderItem = OrderItem.Create(dto.OrderId, dto.ProductId, dto.Quantity, dto.UnitPriceInOrder);
        await orderItemRepository.AddAsync(newOrderItem);

        return new OrderItemDto
        {
            Id = newOrderItem.Id,
            OrderId = newOrderItem.OrderId,
            ProductId = newOrderItem.ProductId,
            Quantity = newOrderItem.Quantity,
            UnitPriceInOrder = newOrderItem.UnitPriceInOrder
        };
    }
}
