using AutoMapper;
using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class ClothingItemService : IClothingItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public ClothingItemService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }
    
    public async Task<ClothingItemPhotoDto> AddPhotoByClothingItem(ImageUploadResult result, Guid clothingItemId)
    {
        var clothingItem = await _unitOfWork.ClothingItemRepository.GetClothingByIdAsync(clothingItemId);
        if (clothingItem == null) throw new NotFoundException("Clothing item not found!");

        var photo = new ClothingItemPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        clothingItem.ClothingItemPhotos.Add(photo);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ClothingItemPhotoDto>(photo);
    }

    public async Task SetMainClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId)
    {
        var clothingItem = await _unitOfWork.ClothingItemRepository.GetClothingByIdAsync(clothingItemId);
        if (clothingItem == null) throw new NotFoundException("Clothing item not found!");

        var photo = clothingItem.ClothingItemPhotos.FirstOrDefault(p => p.Id == clothingItemPhotoId);
        if (photo == null) throw new NotFoundException("Photo not found!");

        var currentMain = clothingItem.ClothingItemPhotos.FirstOrDefault(p => p.IsMain);
        if (currentMain != null) currentMain.IsMain = false;

        photo.IsMain = true;
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteClothingItemPhotoByClothingItem(Guid clothingItemPhotoId, Guid clothingItemId)
    {
        var clothingItem = await _unitOfWork.ClothingItemRepository.GetClothingByIdAsync(clothingItemId);

        var photo = await _unitOfWork.PhotoRepository.GetClothingItemPhotoByIdAsync(clothingItemPhotoId);

        if (photo == null) throw new NotFoundException("Not Found!");

        if (photo.IsMain) throw new ForbiddenException("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) throw new ConflictException("Error!");
        }

        clothingItem.ClothingItemPhotos.Remove(photo);

        await _unitOfWork.SaveAsync();
    }
}