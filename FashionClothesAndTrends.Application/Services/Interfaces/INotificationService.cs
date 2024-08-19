using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface INotificationService
{
    Task AddNotificationAsync(NotificationDto notificationDto);
    Task MarkAsReadAsync(Guid notificationId);
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId);
    Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(string userId);
    Task<IEnumerable<NotificationDto>> GetDiscountNotificationsForWishlistAsync(Guid wishlistId);
}