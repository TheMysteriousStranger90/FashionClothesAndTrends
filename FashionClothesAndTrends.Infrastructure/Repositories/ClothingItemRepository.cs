using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class ClothingItemRepository : GenericRepository<ClothingItem>, IClothingItemRepository
{
    public ClothingItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ClothingItem> GetClothingByIdAsync(Guid id)
    {
        return await _context.ClothingItems
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .Include(c => c.Ratings)
            .Include(c => c.Comments)
            .Include(c => c.FavoriteItems)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IReadOnlyList<ClothingItem>> GetClothingAsync()
    {
        return await _context.ClothingItems
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ClothingBrand>> GetClothingBrandsAsync()
    {
        return await _context.ClothingBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ClothingItem>> GetClothingByGenderAsync(Gender gender)
    {
        return await _context.ClothingItems
            .Where(c => c.Gender == gender)
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ClothingItem>> GetClothingBySizeAsync(Size size)
    {
        return await _context.ClothingItems
            .Where(c => c.Size == size)
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ClothingItem>> GetClothingByCategoryAsync(Category category)
    {
        return await _context.ClothingItems
            .Where(c => c.Category == category)
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ClothingItem>> GetClothingByFiltersAsync(Gender? gender = null, Size? size = null, Category? category = null)
    {
        var query = _context.ClothingItems.AsQueryable();

        if (gender.HasValue)
        {
            query = query.Where(c => c.Gender == gender);
        }

        if (size.HasValue)
        {
            query = query.Where(c => c.Size == size);
        }

        if (category.HasValue)
        {
            query = query.Where(c => c.Category == category);
        }

        return await query
            .Include(c => c.ClothingItemPhotos) 
            .Include(c => c.ClothingBrand)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<ClothingItem>> GetAllClothingItemsAsync()
    {
        return await _context.ClothingItems
            .Include(c => c.ClothingBrand)
            .Include(c => c.ClothingItemPhotos)
            .Include(c => c.Ratings)
            .Include(c => c.Comments)
            .Include(c => c.FavoriteItems)
            .ToListAsync();
    }
}
