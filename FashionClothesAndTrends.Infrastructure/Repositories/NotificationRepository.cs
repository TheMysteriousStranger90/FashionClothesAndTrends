using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Notification>> GetNotificationsByUserIdAsync(string userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Notification>> GetUnreadNotificationsByUserIdAsync(string userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();
    }
    
    public async Task<bool> AddNotificationAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        return await _context.SaveChangesAsync() > 0;
    }
}
