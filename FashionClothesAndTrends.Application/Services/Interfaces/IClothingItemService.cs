using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IClothingItemService
{
    Task<ClothingItemPhotoDto> AddPhotoByClothingItem(ImageUploadResult result, Guid clothingItemId);
    Task SetMainClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId);
    Task DeleteClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId);
}