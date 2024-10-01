using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Specifications;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IClothingItemService
{
    Task<ClothingItemDto> GetClothingItemById(Guid clothingItemId);
    Task<Pagination<ClothingItemDto>> GetClothingItems(ClothingSpecParams clothingSpecParams);
    Task<IReadOnlyList<ClothingBrandDto>> GetClothingBrands();
    Task<ClothingItemPhotoDto> AddPhotoByClothingItem(ImageUploadResult result, Guid clothingItemId);
    Task SetMainClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId);
    Task DeleteClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId);
    Task AddClothingBrandAsync(CreateClothingBrandDto createClothingBrandDto);
    Task AddClothingItemAsync(ClothingItemDto clothingItemDto);
    Task<IReadOnlyList<ClothingItemDto>> GetAllClothingItemsAsync();
}