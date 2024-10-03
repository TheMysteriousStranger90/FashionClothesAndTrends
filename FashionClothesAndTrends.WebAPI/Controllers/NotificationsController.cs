using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class NotificationsController : BaseApiController
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("user/notifications")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId()
    {
        try
        {
            var userId = User.GetUserId();
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user/notifications/unread")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnreadNotificationsByUserId()
    {
        try
        {
            var userId = User.GetUserId();
            var notifications = await _notificationService.GetUnreadNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("mark-as-read")]
    public async Task<ActionResult> MarkNotificationAsRead([FromBody] Guid notificationId)
    {
        try
        {
            var userId = User.GetUserId();
            await _notificationService.MarkNotificationAsReadAsync(userId, notificationId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}