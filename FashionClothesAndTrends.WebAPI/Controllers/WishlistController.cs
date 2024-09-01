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

    [HttpGet("user/{userId}")]
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

    [HttpGet("user/{userId}/name/{name}")]
    public async Task<ActionResult<WishlistDto>> GetWishlistByName(string name)
    {
        try
        {
            var userId = User.GetUserId();
            var wishlist = await _wishlistService.GetWishlistByNameAsync(userId, name);
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

    [HttpPost]
    public async Task<ActionResult<WishlistDto>> CreateWishlist(string userId, string name)
    {
        try
        {
            var wishlist = await _wishlistService.CreateWishlistAsync(userId, name);
            return CreatedAtAction(nameof(GetWishlistByName), new { userId, name }, wishlist);
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

    [HttpPost("{wishlistId}/items")]
    public async Task<ActionResult<WishlistItemDto>> AddItemToWishlist(Guid wishlistId, Guid clothingItemId)
    {
        try
        {
            var wishlistItem = await _wishlistService.AddItemToWishlistAsync(wishlistId, clothingItemId);
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