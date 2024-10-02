using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, Guid delieveryMethod, string basketId,
        AddressAggregate shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    Task<OrderDto> EditUserOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto);
    Task<IReadOnlyList<OrderToReturnDto>> GetOrdersByUserEmailAsync(string buyerEmail);
    Task<IReadOnlyList<Order>> GetAllOrdersAsync();
}