using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, Guid delieveryMethod, string basketId,
        AddressAggregate shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}