using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface INotificationRepository
{
    Task<IReadOnlyList<Notification>> GetNotificationsByUserIdAsync(string userId);
    Task<IReadOnlyList<Notification>> GetUnreadNotificationsByUserIdAsync(string userId);
    Task<IReadOnlyList<Notification>> GetDiscountNotificationsForWishlistAsync(Guid wishlistId);
    Task<bool> AddNotificationAsync(Notification notification);
    Task<bool> MarkAsReadAsync(Guid notificationId); 
}