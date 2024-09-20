using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class WishlistController : BaseApiController
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpPost("{wishlistName}")]
    public async Task<ActionResult> CreateWishlist(string wishlistName)
    {
        try
        {
            var userId = User.GetUserId();
            await _wishlistService.CreateWishlistAsync(userId, wishlistName);
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

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlistsByUserId()
    {
        try
        {
            var userId = User.GetUserId();
            var wishlists = await _wishlistService.GetWishlistsByUserIdAsync(userId);
            return Ok(wishlists);
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

    [HttpGet("user/{userId}/name/{wishlistName}")]
    public async Task<ActionResult<WishlistDto>> GetWishlistByName(string wishlistName)
    {
        try
        {
            var userId = User.GetUserId();
            var wishlist = await _wishlistService.GetWishlistByNameAsync(userId, wishlistName);
            return Ok(wishlist);
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

    [HttpDelete("{wishlistId}")]
    public async Task<ActionResult> DeleteWishlist(Guid wishlistId)
    {
        try
        {
            await _wishlistService.DeleteWishlistAsync(wishlistId);
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

    [HttpPost("items")]
    public async Task<ActionResult<WishlistItemDto>> AddItemToWishlist(Guid clothingItemId, string? wishlistName = null)
    {
        try
        {
            var userId = User.GetUserId();
            var wishlistItem = await _wishlistService.AddItemToWishlistAsync(userId, clothingItemId, wishlistName);
            return Ok(wishlistItem);
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

    [HttpDelete("{wishlistId}/items/{itemId}")]
    public async Task<ActionResult> RemoveItemFromWishlist(Guid wishlistId, Guid itemId)
    {
        try
        {
            await _wishlistService.RemoveItemFromWishlistAsync(wishlistId, itemId);
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
}