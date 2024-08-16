using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IOrderHistoryService
{
    Task<OrderHistoryDto> CreateOrderHistoryAsync(OrderDto order);
    Task<IReadOnlyList<OrderHistoryDto>> GetOrderHistoriesForUserAsync(string userId);
    Task<OrderHistoryDto> GetOrderHistoryByIdAsync(Guid id);
}
