using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class CouponController : BaseApiController
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<ActionResult<CouponDto>> CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            var createdCoupon = await _couponService.CreateCouponAsync(couponDto);
            return Ok(createdCoupon);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("apply")]
    public async Task<ActionResult> ApplyCouponToClothingItem([FromBody] ApplyCouponDto applyCouponDto)
    {
        try
        {
            await _couponService.ApplyCouponToClothingItemAsync(applyCouponDto.ClothingItemId,
                applyCouponDto.CouponCode);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}