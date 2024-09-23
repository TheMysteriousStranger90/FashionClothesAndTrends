using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.WebAPI.Errors;
using FashionClothesAndTrends.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionClothesAndTrends.WebAPI.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IUserService userService, IMapper mapper)
    {
        _mapper = mapper;
        _orderService = orderService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
    {
        try
        {
            var email = User.GetUserEmail();
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new ApiResponse(400, "User email is null or empty"));
            }

            var address = _mapper.Map<AddressDto, AddressAggregate>(orderDto.ShipToAddress);
            if (address == null)
            {
                return BadRequest(new ApiResponse(400, "Address mapping failed"));
            }

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId,
                address);

            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Problem creating order"));
            }

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        try
        {
            var email = User.GetUserEmail();
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(Guid id)
    {
        try
        {
            var email = User.GetUserEmail();
            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [HttpGet("delivery-methods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        try
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("user-orders")]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersByUserEmailAsync(
        [FromQuery] string userName)
    {
        try
        {
            var userDto = await _userService.GetUserByUsernameAsync(userName);
            if (userDto == null)
            {
                return NotFound(new ApiResponse(404, $"User with username '{userName}' not found."));
            }

            var user = _mapper.Map<User>(userDto);
            var orders = await _orderService.GetOrdersByUserEmailAsync(user.Email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("{orderId}")]
    public async Task<ActionResult<OrderDto>> EditUserOrderAsync(Guid orderId, OrderUpdateDto orderUpdateDto)
    {
        try
        {
            var updatedOrder = await _orderService.EditUserOrderAsync(orderId, orderUpdateDto);
            return Ok(updatedOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }
}