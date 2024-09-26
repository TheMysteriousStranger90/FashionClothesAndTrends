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

    public async Task AddNotificationAsync(Notification notification)
    {
        await _unitOfWork.NotificationRepository.AddNotificationAsync(notification);
        await _unitOfWork.SaveAsync();
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
}