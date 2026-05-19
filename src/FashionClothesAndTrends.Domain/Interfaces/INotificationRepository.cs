using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetNotificationsByUserIdAsync(string userId);
    Task<IReadOnlyList<Notification>> GetUnreadNotificationsByUserIdAsync(string userId);
    Task<bool> AddNotificationAsync(Notification notification);
}