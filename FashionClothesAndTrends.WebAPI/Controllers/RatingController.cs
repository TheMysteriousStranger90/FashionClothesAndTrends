using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class RatingController : BaseApiController
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpPost]
    public async Task<ActionResult> AddRating(RatingDto ratingDto)
    {
        try
        {
            await _ratingService.AddRatingAsync(ratingDto);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }
    
    [HttpGet("clothing/{clothingItemId}/average")]
    public async Task<ActionResult<double?>> GetAverageRating(Guid clothingItemId)
    {
        try
        {
            var averageRating = await _ratingService.GetAverageRatingAsync(clothingItemId);
            return Ok(averageRating);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }
    
    [HttpGet("user-rating/{clothingItemId}")]
    public async Task<ActionResult<RatingDto?>> GetUserRating(Guid clothingItemId)
    {
        try
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var rating = await _ratingService.GetUserRatingAsync(userId, clothingItemId);
            return Ok(rating);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, "An error occurred while processing your request"));
        }
    }
}