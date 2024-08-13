using System.Text.Json;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using StackExchange.Redis;

namespace FashionClothesAndTrends.Application.Services;

public class BasketService : IBasketService
{
    private readonly IDatabase _database;

    public BasketService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }
}