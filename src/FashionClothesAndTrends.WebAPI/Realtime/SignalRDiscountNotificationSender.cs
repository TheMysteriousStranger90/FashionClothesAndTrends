using FashionClothesAndTrends.Application.Hubs.Interfaces;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FashionClothesAndTrends.WebAPI.Realtime;

public class SignalRDiscountNotificationSender : IDiscountNotificationSender
{
    private readonly IHubContext<DiscountNotificationHub, INotificationHub> _hubContext;

    public SignalRDiscountNotificationSender(IHubContext<DiscountNotificationHub, INotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendDiscountNotificationAsync(string userId, Notification notification)
    {
        await _hubContext.Clients.Group(userId).SendMessage(notification);
    }
}
