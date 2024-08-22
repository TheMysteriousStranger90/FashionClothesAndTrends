using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IPhotoRepository
{
    Task<UserPhoto> GetUserPhotoByIdAsync(Guid userPhotoId);
    Task<ClothingItemPhoto> GetClothingItemPhotoByIdAsync(Guid clothingItemPhotoId);
    
    Task<UserPhoto> GetUserPhotoByIdAndUserNameAsync(Guid userPhotoId, string appUserName);
    Task<ClothingItemPhoto> GetClothingItemByIdAndClothingItemIdAsync(Guid clothingItemPhotoId, Guid clothingItemId);
    
    void RemoveUserPhoto(UserPhoto photo);
    void RemoveClothingItemPhoto(ClothingItemPhoto photo);
}