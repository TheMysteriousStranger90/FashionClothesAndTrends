using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class OrderHistoryRepository : GenericRepository<OrderHistory>, IOrderHistoryRepository
{
    public OrderHistoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderHistory>> GetOrderHistoryByUserIdAsync(string userId)
    {
        return await _context.OrderHistories
            .Include(f => f.OrderItems)
            .Include(f => f.User)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<OrderHistory> GetByIdAsync(Guid id)
    {
        return await _context.OrderHistories
            .Include(f => f.OrderItems)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IReadOnlyList<OrderHistory>> ListAllAsync()
    {
        return await _context.OrderHistories
            .Include(f => f.OrderItems)
            .ToListAsync();
    }
}