using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByUsernameAsync(string userName);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserPhotoDto> AddPhotoByUser(ImageUploadResult result, string userName);
    Task SetMainUserPhotoByUser(Guid userPhotoId, string userName);
    Task DeleteUserPhotoByUser(Guid userPhotoId, string userName);
}