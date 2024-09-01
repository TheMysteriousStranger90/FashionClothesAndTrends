using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class FavoritesController : BaseApiController
{
    private readonly IFavoriteItemService _favoriteItemService;

    public FavoritesController(IFavoriteItemService favoriteItemService)
    {
        _favoriteItemService = favoriteItemService;
    }

    [HttpPost("{clothingItemId}")]
    public async Task<ActionResult> AddFavorite(Guid clothingItemId)
    {
        try
        {
            var userId = User.GetUserId();
            await _favoriteItemService.AddFavoriteAsync(clothingItemId, userId);
            return Ok();
        }
        catch (ConflictException ex)
        {
            return Conflict(new ApiResponse(409, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }

    [HttpDelete("{clothingItemId}")]
    public async Task<ActionResult> RemoveFavorite(Guid clothingItemId)
    {
        try
        {
            var userId = User.GetUserId();
            await _favoriteItemService.RemoveFavoriteAsync(clothingItemId, userId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<FavoriteItemDto>>> GetFavoritesByUserId()
    {
        try
        {
            var userId = User.GetUserId();
            var favoriteItems = await _favoriteItemService.GetFavoritesByUserIdAsync(userId);
            return Ok(favoriteItems);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse(404, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }

    [HttpGet("{clothingItemId}/isFavorite")]
    public async Task<ActionResult<bool>> IsFavorite(Guid clothingItemId)
    {
        try
        {
            var userId = User.GetUserId();
            var isFavorite = await _favoriteItemService.IsFavoriteAsync(clothingItemId, userId);
            return Ok(isFavorite);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }
}