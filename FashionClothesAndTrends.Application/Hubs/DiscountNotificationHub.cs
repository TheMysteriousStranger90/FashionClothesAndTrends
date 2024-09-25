using FashionClothesAndTrends.Application.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FashionClothesAndTrends.Application.Hubs;

public class DiscountNotificationHub : Hub<INotificationHub>
{
    public Task SubscribeToUser(string userId)
    {
        return this.Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }
}