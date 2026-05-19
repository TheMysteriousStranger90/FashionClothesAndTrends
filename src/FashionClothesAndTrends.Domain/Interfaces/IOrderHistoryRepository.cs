using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IOrderHistoryRepository : IGenericRepository<OrderHistory>
{
    Task<IEnumerable<OrderHistory>> GetOrderHistoryByUserIdAsync(string userId);
    new Task<OrderHistory?> GetByIdAsync(Guid id);
    new Task<IReadOnlyList<OrderHistory>> ListAllAsync();
}