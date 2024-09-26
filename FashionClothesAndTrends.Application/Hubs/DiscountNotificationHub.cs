using FashionClothesAndTrends.Application.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FashionClothesAndTrends.Application.Hubs;

public class DiscountNotificationHub : Hub<INotificationHub>
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        if (exception != null)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
        await base.OnDisconnectedAsync(exception);
    }

    public Task SubscribeToUser(string userId)
    {
        Console.WriteLine($"Subscribing client {Context.ConnectionId} to group {userId}");
        return this.Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }
}