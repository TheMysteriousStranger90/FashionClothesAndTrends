using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IOrderHistoryRepository : IGenericRepository<OrderHistory>
{
    Task<IEnumerable<OrderHistory>> GetOrderHistoryByUserIdAsync(string userId);
    Task<OrderHistory> GetByIdAsync(Guid id);
    Task<IReadOnlyList<OrderHistory>> ListAllAsync();
}