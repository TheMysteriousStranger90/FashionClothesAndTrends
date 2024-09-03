using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task NotifyUserAboutDiscountAsync(string userId, Guid clothingItemId)
    {
        var notification = new Notification
        {
            Text = "Item in your wishlist has a discount!",
            UserId = userId,
            IsRead = false
        };
        await _unitOfWork.NotificationRepository.AddNotificationAsync(notification);
    }

    public async Task AddNotificationAsync(NotificationDto notificationDto)
    {
        if (notificationDto == null)
        {
            throw new ArgumentNullException(nameof(notificationDto));
        }

        var notification = _mapper.Map<Notification>(notificationDto);
        var result = await _unitOfWork.NotificationRepository.AddNotificationAsync(notification);

        if (!result)
        {
            throw new InternalServerException("Failed to add notification.");
        }
    }

    public async Task MarkAsReadAsync(Guid notificationId)
    {
        var result = await _unitOfWork.NotificationRepository.MarkAsReadAsync(notificationId);

        if (!result)
        {
            throw new NotFoundException("Notification not found.");
        }
    }

    public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetNotificationsByUserIdAsync(userId);

        if (notifications == null || !notifications.Any())
        {
            throw new NotFoundException("No notifications found for this user.");
        }

        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(string userId)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetUnreadNotificationsByUserIdAsync(userId);

        if (notifications == null || !notifications.Any())
        {
            throw new NotFoundException("No unread notifications found for this user.");
        }

        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<IEnumerable<NotificationDto>> GetDiscountNotificationsForWishlistAsync(Guid wishlistId)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetDiscountNotificationsForWishlistAsync(wishlistId);

        if (notifications == null || !notifications.Any())
        {
            throw new NotFoundException("No discount notifications found for this wishlist.");
        }

        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }
}