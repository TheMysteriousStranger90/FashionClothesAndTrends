using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Specifications;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Helpers;
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
    public async Task<ActionResult<IReadOnlyList<ClothingBrand>>> GetClothingBrandS()
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
}