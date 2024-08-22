using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly ApplicationDbContext _context;

    public PhotoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserPhoto> GetUserPhotoByIdAsync(Guid userPhotoId)
    {
        return await _context.UserPhotos.FindAsync(userPhotoId);
    }

    public async Task<ClothingItemPhoto> GetClothingItemPhotoByIdAsync(Guid clothingItemPhotoId)
    {
        return await _context.ClothingItemPhotos.FindAsync(clothingItemPhotoId);
    }

    public async Task<UserPhoto> GetUserPhotoByIdAndUserNameAsync(Guid userPhotoId, string appUserName)
    {
        return await _context.UserPhotos
            .Include(up => up.User)
            .FirstOrDefaultAsync(up => up.Id == userPhotoId && up.User.UserName == appUserName);
    }

    public async Task<ClothingItemPhoto> GetClothingItemByIdAndClothingItemIdAsync(Guid clothingItemPhotoId, Guid clothingItemId)
    {
        return await _context.ClothingItemPhotos
            .Include(cp => cp.ClothingItem)
            .FirstOrDefaultAsync(cp => cp.Id == clothingItemPhotoId && cp.ClothingItemId == clothingItemId);
    }

    public void RemoveUserPhoto(UserPhoto photo)
    {
        _context.UserPhotos.Remove(photo);
    }

    public void RemoveClothingItemPhoto(ClothingItemPhoto photo)
    {
        _context.ClothingItemPhotos.Remove(photo);
    }
}