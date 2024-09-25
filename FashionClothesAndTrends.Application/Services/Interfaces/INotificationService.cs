using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface INotificationService
{
    Task AddNotificationAsync(Notification notification);
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId);
    Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(string userId);
}