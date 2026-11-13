using InventoryHub.Application.Dto.Order;
using InventoryHub.Application.Dto.OrderItem;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IOrderItemRepository orderItemRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<PaginationResponseDto<OrderDto>> GetAllAsync(PaginationRequestDto<OrderSortBy>  request)
    {
        var response = await orderRepository.GetAllAsync(request);

        return new PaginationResponseDto<OrderDto>
        {
            Items = response.Items.Select(o => new OrderDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    OrderDate = o.OrderDate,
                    Status = o.Status
                })
                .ToList(),
            Page = response.Page,
            Total = response.Total,
        };
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
            var existingProduct = await productRepository.GetByIdAsync(item.ProductId);
            if (existingProduct == null)
            {
                throw new NotFoundException($"There are no product with id {item.ProductId}");
            }

            var product = await productRepository.DecrementStocks(item.ProductId, item.Quantity);
            if (product == null)
            {
                throw new ConflictException(
                    $"Not enough stock for product {existingProduct.Name}: requested {item.Quantity}, available {existingProduct.StockQuantity}.");
            }

            var newItem = OrderItem.Create(newOrder.Id, item.ProductId, item.Quantity, product.Price);
            await orderItemRepository.AddAsync(newItem);
        }

        await unitOfWork.SaveChangesAsync();

        return new OrderDto
        {
            Id = newOrder.Id,
            CustomerId = newOrder.CustomerId,
            OrderDate = newOrder.OrderDate,
            Status = newOrder.Status
        };
    }

    public async Task<OrderSummaryDto> GetOrderSummary(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null) throw new NotFoundException("There are no order");
        var orderItems = order.OrderItems;
        var sum = orderItems.Sum(i => i.UnitPriceInOrder * i.Quantity);

        var orderItemsDto = orderItems.Select(item => new OrderItemDto
        {
            Id = item.Id,
            OrderId = order.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPriceInOrder = item.UnitPriceInOrder
        }).ToList();


        return new OrderSummaryDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            Status = order.Status,
            OrderItems = orderItemsDto,
            TotalSum = sum,
            ItemsCount = orderItems.Count()
        };
    }
}
