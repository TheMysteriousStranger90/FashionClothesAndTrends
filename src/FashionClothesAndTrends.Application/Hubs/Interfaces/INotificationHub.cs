using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Hubs.Interfaces;

public interface INotificationHub
{
    public Task SendMessage(Notification notification);
}