using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Specifications;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

public class ClothingController : BaseApiController
{
    private readonly IClothingItemService _clothingItemService;

    public ClothingController(IClothingItemService clothingItemService)
    {
        _clothingItemService = clothingItemService;
    }

    [Cached(600)]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClothingItemDto>> GetClothingItem(Guid id)
    {
        try
        {
            return Ok(await _clothingItemService.GetClothingItemById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Cached(600)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ClothingItemDto>>> GetClothingItems(
        [FromQuery] ClothingSpecParams clothingSpecParams)
    {
        try
        {
            return Ok(await _clothingItemService.GetClothingItems(clothingSpecParams));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ClothingBrandDto>>> GetClothingBrandS()
    {
        try
        {
            return Ok(await _clothingItemService.GetClothingBrands());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("brands")]
    public async Task<ActionResult> AddClothingBrandAsync([FromBody] ClothingBrandDto clothingBrandDto)
    {
        try
        {
            await _clothingItemService.AddClothingBrandAsync(clothingBrandDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("items")]
    public async Task<ActionResult> AddClothingItemAsync([FromBody] ClothingItemDto clothingItemDto)
    {
        try
        {
            await _clothingItemService.AddClothingItemAsync(clothingItemDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyList<ClothingItemDto>>> GetAllClothingItems()
    {
        try
        {
            var clothingItems = await _clothingItemService.GetAllClothingItemsAsync();
            return Ok(clothingItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}