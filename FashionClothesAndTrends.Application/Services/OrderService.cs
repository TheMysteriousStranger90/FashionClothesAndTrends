using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.Domain.Specifications;

namespace FashionClothesAndTrends.Application.Services;

public class OrderService : IOrderService
{
    private readonly IBasketService _basketService;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketService basketService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _basketService = basketService;   }

    public async Task<Order> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, string basketId,
        AddressAggregate shippingAddress)
    {
        var basket = await _basketService.GetBasketAsync(basketId);

        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.GenericRepository<ClothingItem>().GetByIdAsync(item.Id);
            var itemOrdered = new ClothingItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        var deliveryMethod = await _unitOfWork.GenericRepository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        var subtotal = items.Sum(item => item.Price * item.Quantity);

        var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
        var order = await _unitOfWork.GenericRepository<Order>().GetEntityWithSpec(spec);

        if (order != null)
        {
            order.ShipToAddress = shippingAddress;
            order.DeliveryMethod = deliveryMethod;
            order.Subtotal = subtotal;
            _unitOfWork.GenericRepository<Order>().Update(order);
        }
        else
        {
            order = new Order(items, buyerEmail, shippingAddress, deliveryMethod,
                subtotal, basket.PaymentIntentId);
            _unitOfWork.GenericRepository<Order>().Add(order);
        }

        var result = await _unitOfWork.SaveAsync();

        if (result <= 0) return null;

        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.GenericRepository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

        return await _unitOfWork.GenericRepository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.GenericRepository<Order>().ListAsync(spec);
    }
}