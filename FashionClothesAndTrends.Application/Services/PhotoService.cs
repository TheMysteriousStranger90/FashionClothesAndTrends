using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FashionClothesAndTrends.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        var acc = new Account
        (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<UserPhotoDto> GetUserPhotoByIdAsync(Guid userPhotoId)
    {
        var userPhoto = await _unitOfWork.PhotoRepository.GetUserPhotoByIdAsync(userPhotoId);

        if (userPhoto == null)
        {
            throw new NotFoundException($"User photo not found.");
        }

        return _mapper.Map<UserPhotoDto>(userPhoto);
    }

    public async Task<ClothingItemPhotoDto> GetClothingItemPhotoByIdAsync(Guid clothingItemPhotoId)
    {
        var clothingItemPhoto = await _unitOfWork.PhotoRepository.GetClothingItemPhotoByIdAsync(clothingItemPhotoId);

        if (clothingItemPhoto == null)
        {
            throw new NotFoundException($"Clothing item photo not found.");
        }

        return _mapper.Map<ClothingItemPhotoDto>(clothingItemPhoto);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "FashionClothesAndTrends"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task DeleteUserPhotoByIdAsync(Guid userPhotoId)
    {
        var userPhoto = await _unitOfWork.PhotoRepository.GetUserPhotoByIdAsync(userPhotoId);
        if (userPhoto == null) throw new NotFoundException("User photo does not exist");

        if (userPhoto.PublicId != null)
        {
            var deleteParams = new DeletionParams(userPhoto.PublicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result == "ok")
            {
                _unitOfWork.PhotoRepository.RemoveUserPhoto(userPhoto);
            }
        }
        else
        {
            _unitOfWork.PhotoRepository.RemoveUserPhoto(userPhoto);
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteClothingItemPhotoByIdAsync(Guid clothingItemPhotoId)
    {
        var clothingItemPhoto = await _unitOfWork.PhotoRepository.GetClothingItemPhotoByIdAsync(clothingItemPhotoId);
        if (clothingItemPhoto == null) throw new NotFoundException("Clothing item photo photo does not exist");

        if (clothingItemPhoto.PublicId != null)
        {
            var deleteParams = new DeletionParams(clothingItemPhoto.PublicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result == "ok")
            {
                _unitOfWork.PhotoRepository.RemoveClothingItemPhoto(clothingItemPhoto);
            }
        }
        else
        {
            _unitOfWork.PhotoRepository.RemoveClothingItemPhoto(clothingItemPhoto);
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(deleteParams);
    }
}