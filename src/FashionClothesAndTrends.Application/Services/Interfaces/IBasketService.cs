using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IBasketService
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}