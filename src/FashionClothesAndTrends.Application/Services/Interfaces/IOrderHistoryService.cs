using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IOrderHistoryService
{
    Task CreateOrderHistoryAsync(OrderHistoryDto order, string userId);
    Task<IReadOnlyList<OrderHistoryToReturnDto>> GetOrderHistoriesByUserIdAsync(string userId);
    Task<OrderHistoryToReturnDto> GetOrderHistoryByIdAsync(Guid id);
    Task<IReadOnlyList<OrderHistoryToReturnDto>> GatAllOrderHistoriesAsync();
}
