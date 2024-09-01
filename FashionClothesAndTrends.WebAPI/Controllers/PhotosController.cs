using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class PhotosController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IClothingItemService _clothingItemService;
    private readonly IPhotoService _photoService;

    public PhotosController(IUserService userService, IClothingItemService clothingItemService,
        IPhotoService photoService)
    {
        _userService = userService;
        _clothingItemService = clothingItemService;
        _photoService = photoService;
    }

    [HttpGet("user/{photoId}")]
    public async Task<ActionResult<UserPhotoDto>> GetUserPhotoById(Guid photoId)
    {
        try
        {
            var photoDto = await _photoService.GetUserPhotoByIdAsync(photoId);
            return Ok(photoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("user")]
    public async Task<ActionResult<UserPhotoDto>> AddUserPhoto(IFormFile file)
    {
        try
        {
            var userName = User.GetUserName();
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photoDto = await _userService.AddPhotoByUser(result, userName);
            return Ok(photoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("user/{photoId}/set-main")]
    public async Task<ActionResult> SetMainUserPhoto(Guid photoId)
    {
        try
        {
            var userName = User.GetUserName();
            await _userService.SetMainUserPhotoByUser(photoId, userName);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("user/{photoId}")]
    public async Task<ActionResult> DeleteUserPhoto(Guid photoId)
    {
        try
        {
            var userName = User.GetUserName();
            await _userService.DeleteUserPhotoByUser(photoId, userName);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("clothing-item/{photoId}")]
    public async Task<ActionResult<ClothingItemPhotoDto>> GetClothingItemPhotoById(Guid photoId)
    {
        try
        {
            var photoDto = await _photoService.GetClothingItemPhotoByIdAsync(photoId);
            return Ok(photoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("clothing-item/{clothingItemId}")]
    public async Task<ActionResult<ClothingItemPhotoDto>> AddClothingItemPhoto(Guid clothingItemId, IFormFile file)
    {
        try
        {
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photoDto = await _clothingItemService.AddPhotoByClothingItem(result, clothingItemId);
            return Ok(photoDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("clothing-item/{clothingItemId}/photo/{photoId}/set-main")]
    public async Task<ActionResult> SetMainClothingItemPhoto(Guid clothingItemId, Guid photoId)
    {
        try
        {
            await _clothingItemService.SetMainClothingItemPhotoByClothingItem(photoId, clothingItemId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("clothing-item/{clothingItemId}/photo/{photoId}")]
    public async Task<ActionResult> DeleteClothingItemPhoto(Guid clothingItemId, Guid photoId)
    {
        try
        {
            await _clothingItemService.DeleteClothingItemPhotoByClothingItem(photoId, clothingItemId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}