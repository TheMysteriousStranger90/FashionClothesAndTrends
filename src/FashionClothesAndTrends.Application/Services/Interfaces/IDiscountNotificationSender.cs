using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IDiscountNotificationSender
{
    Task SendDiscountNotificationAsync(string userId, Notification notification);
}
