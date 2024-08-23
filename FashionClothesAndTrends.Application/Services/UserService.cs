using AutoMapper;
using CloudinaryDotNet.Actions;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }

    public async Task<UserDto> GetUserByUsernameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);
        if (user == null) throw new NotFoundException("Not Found!");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmail(email);
        if (user == null) throw new NotFoundException("Not Found!");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByIdAsync(string id)
    {
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
        if (user == null) throw new NotFoundException("Not Found!");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserPhotoDto> AddPhotoByUser(ImageUploadResult result, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);

        if (user == null) throw new NotFoundException("Not Found!");

        var photo = new UserPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.UserPhotos.Add(photo);

        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserPhotoDto>(photo);
    }

    public async Task SetMainUserPhotoByUser(Guid userPhotoId, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);

        if (user == null) throw new NotFoundException("Not Found!");

        var photo = user.UserPhotos.FirstOrDefault(x => x.Id == userPhotoId);

        if (photo == null) throw new NotFoundException("Not Found!");

        if (photo.IsMain) throw new ForbiddenException("This is already your main photo!");

        var currentMain = user.UserPhotos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteUserPhotoByUser(Guid userPhotoId, string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(userName);

        var photo = await _unitOfWork.PhotoRepository.GetUserPhotoByIdAsync(userPhotoId);

        if (photo == null) throw new NotFoundException("Not Found!");

        if (photo.IsMain) throw new ForbiddenException("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) throw new ConflictException("Error!");
        }

        user.UserPhotos.Remove(photo);

        await _unitOfWork.SaveAsync();
    }
}