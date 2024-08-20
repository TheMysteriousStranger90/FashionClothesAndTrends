using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.Domain.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace FashionClothesAndTrends.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketService _basketService;
    private readonly IConfiguration _config;

    public PaymentService(IUnitOfWork unitOfWork, IBasketService basketService,
        IConfiguration config)
    {
        _config = config;
        _basketService = basketService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        var basket = await _basketService.GetBasketAsync(basketId);

        if (basket == null) throw new NotFoundException("No basket found.");;

        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.GenericRepository<DeliveryMethod>()
                .GetByIdAsync((Guid)basket.DeliveryMethodId);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.GenericRepository<ClothingItem>().GetByIdAsync(item.Id);
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent intent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketService.UpdateBasketAsync(basket);

        return basket;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.GenericRepository<Order>().GetEntityWithSpec(spec);

        if (order == null) throw new NotFoundException("No order found.");

        order.Status = OrderStatus.PaymentFailed;
        await _unitOfWork.SaveAsync();

        return order;
    }

    public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.GenericRepository<Order>().GetEntityWithSpec(spec);

        if (order == null) throw new NotFoundException("No order found.");

        order.Status = OrderStatus.PaymentReceived;
        await _unitOfWork.SaveAsync();

        return order;
    }
}