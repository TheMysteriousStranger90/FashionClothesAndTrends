using FashionClothesAndTrends.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IPhotoService
{
    Task<UserPhotoDto> GetUserPhotoByIdAsync(Guid userPhotoId);
    Task<ClothingItemPhotoDto> GetClothingItemPhotoByIdAsync(Guid clothingItemPhotoId);

    Task<PhotoUploadResultDto> AddPhotoAsync(IFormFile file);

    Task DeleteUserPhotoByIdAsync(Guid userPhotoId);
    Task DeleteClothingItemPhotoByIdAsync(Guid clothingItemPhotoId);
    Task<PhotoDeletionResultDto> DeletePhotoAsync(string publicId);
}
