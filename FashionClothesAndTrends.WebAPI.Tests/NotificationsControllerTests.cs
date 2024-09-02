using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class NotificationsControllerTests
{
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly NotificationsController _controller;

    public NotificationsControllerTests()
    {
        _notificationServiceMock = new Mock<INotificationService>();
        _controller = new NotificationsController(_notificationServiceMock.Object);
    }

    [Fact]
    public async Task AddNotification_ReturnsOkResult()
    {
        // Arrange
        var notificationDto = new NotificationDto { Id = Guid.NewGuid(), Text = "Test notification", IsRead = false };

        // Act
        var result = await _controller.AddNotification(notificationDto);

        // Assert
        _notificationServiceMock.Verify(service => service.AddNotificationAsync(notificationDto), Times.Once);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task MarkAsRead_ReturnsNoContentResult()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        _notificationServiceMock.Setup(service => service.MarkAsReadAsync(notificationId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.MarkAsRead(notificationId);

        // Assert
        _notificationServiceMock.Verify(service => service.MarkAsReadAsync(notificationId), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetNotificationsByUserId_ReturnsOkResult_WithNotifications()
    {
        // Arrange
        var userId = "testuser";
        var notificationsDto = new List<NotificationDto>
        {
            new NotificationDto { Id = Guid.NewGuid(), Text = "Notification 1", IsRead = false },
            new NotificationDto { Id = Guid.NewGuid(), Text = "Notification 2", IsRead = true }
        };
        _notificationServiceMock.Setup(service => service.GetNotificationsByUserIdAsync(userId))
            .ReturnsAsync(notificationsDto);

        // Act
        var result = await _controller.GetNotificationsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedNotifications = Assert.IsType<List<NotificationDto>>(okResult.Value);
        Assert.Equal(notificationsDto.Count, returnedNotifications.Count);
    }

    [Fact]
    public async Task GetUnreadNotificationsByUserId_ReturnsOkResult_WithNotifications()
    {
        // Arrange
        var userId = "testuser";
        var notificationsDto = new List<NotificationDto>
        {
            new NotificationDto { Id = Guid.NewGuid(), Text = "Notification 1", IsRead = false },
            new NotificationDto { Id = Guid.NewGuid(), Text = "Notification 2", IsRead = false }
        };
        _notificationServiceMock.Setup(service => service.GetUnreadNotificationsByUserIdAsync(userId))
            .ReturnsAsync(notificationsDto);

        // Act
        var result = await _controller.GetUnreadNotificationsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedNotifications = Assert.IsType<List<NotificationDto>>(okResult.Value);
        Assert.Equal(notificationsDto.Count, returnedNotifications.Count);
    }

    [Fact]
    public async Task GetDiscountNotificationsForWishlist_ReturnsOkResult_WithNotifications()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var notificationsDto = new List<NotificationDto>
        {
            new NotificationDto { Id = Guid.NewGuid(), Text = "Discount Notification 1", IsRead = false },
            new NotificationDto { Id = Guid.NewGuid(), Text = "Discount Notification 2", IsRead = true }
        };
        _notificationServiceMock.Setup(service => service.GetDiscountNotificationsForWishlistAsync(wishlistId))
            .ReturnsAsync(notificationsDto);

        // Act
        var result = await _controller.GetDiscountNotificationsForWishlist(wishlistId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedNotifications = Assert.IsType<List<NotificationDto>>(okResult.Value);
        Assert.Equal(notificationsDto.Count, returnedNotifications.Count);
    }
}