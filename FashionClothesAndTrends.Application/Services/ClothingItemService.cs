using AutoMapper;
using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Specifications;

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

    public async Task<ClothingItemDto> GetClothingItemById(Guid clothingItemId)
    {
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(clothingItemId);

        var clothingItem = await _unitOfWork.GenericRepository<ClothingItem>().GetEntityWithSpec(spec);

        if (clothingItem == null) throw new  NotFoundException("ClothingItem not found.");

        return _mapper.Map<ClothingItem, ClothingItemDto>(clothingItem);
    }

    public async Task<Pagination<ClothingItemDto>> GetClothingItems(ClothingSpecParams clothingSpecParams)
    {
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(clothingSpecParams);

        var countSpec = new ClothingItemWithFiltersForCountSpecificication(clothingSpecParams);

        var totalItems = await _unitOfWork.GenericRepository<ClothingItem>().CountAsync(countSpec);

        var clothingItems = await _unitOfWork.GenericRepository<ClothingItem>().ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<ClothingItem>, IReadOnlyList<ClothingItemDto>>(clothingItems);
        
        return new Pagination<ClothingItemDto>(clothingSpecParams.PageIndex, clothingSpecParams.PageSize, totalItems, data);
    }

    public async Task<IReadOnlyList<ClothingBrandDto>> GetClothingBrands()
    {
        var brands = await _unitOfWork.GenericRepository<ClothingBrand>().ListAllAsync();
        return _mapper.Map<IReadOnlyList<ClothingBrandDto>>(brands);
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
    
    public async Task AddClothingBrandAsync(CreateClothingBrandDto createClothingBrandDto)
    {
        var clothingBrand = _mapper.Map<ClothingBrand>(createClothingBrandDto);
        await _unitOfWork.GenericRepository<ClothingBrand>().AddAsync(clothingBrand);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task AddClothingItemAsync(ClothingItemDto clothingItemDto)
    {
        var clothingItem = _mapper.Map<ClothingItem>(clothingItemDto);
        await _unitOfWork.GenericRepository<ClothingItem>().AddAsync(clothingItem);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<IReadOnlyList<ClothingItemDto>> GetAllClothingItemsAsync()
    {
        var clothingItems = await _unitOfWork.ClothingItemRepository.GetAllClothingItemsAsync();
        return _mapper.Map<IReadOnlyList<ClothingItemDto>>(clothingItems);
    }
}