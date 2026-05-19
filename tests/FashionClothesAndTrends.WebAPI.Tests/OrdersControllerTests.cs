using System.Security.Claims;
using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class OrdersControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OrdersController _controller;

      public OrdersControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _userServiceMock = new Mock<IUserService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new OrdersController(_orderServiceMock.Object, _userServiceMock.Object, _mapperMock.Object);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, "test@example.com")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest_WhenAddressMappingFails()
    {
        // Arrange
        var orderDto = new OrderDto { ShipToAddress = new AddressDto() };
        _mapperMock.Setup(m => m.Map<AddressDto, AddressAggregate>(It.IsAny<AddressDto>()))
            .Returns((AddressAggregate)null);

        // Act
        var result = await _controller.CreateOrder(orderDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Equal("Address mapping failed", apiResponse.Message);
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest_WhenOrderCreationFails()
    {
        // Arrange
        var orderDto = new OrderDto { ShipToAddress = new AddressDto() };
        var address = new AddressAggregate();
        _mapperMock.Setup(m => m.Map<AddressDto, AddressAggregate>(It.IsAny<AddressDto>()))
            .Returns(address);
        _orderServiceMock.Setup(service => service.CreateOrderAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<AddressAggregate>()))
            .ReturnsAsync((Order)null);

        // Act
        var result = await _controller.CreateOrder(orderDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Equal("Problem creating order", apiResponse.Message);
    }

    [Fact]
    public async Task GetOrdersForUser_ReturnsOrders_WhenOrdersExist()
    {
        // Arrange
        var email = "test@example.com";
        var orders = new List<Order> { new Order() };
        _orderServiceMock.Setup(service => service.GetOrdersForUserAsync(email))
            .ReturnsAsync(orders);
        _mapperMock.Setup(mapper => mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders))
            .Returns(new List<OrderToReturnDto>());

        // Act
        var result = await _controller.GetOrdersForUser();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedOrders = Assert.IsType<List<OrderToReturnDto>>(okResult.Value);
        Assert.NotNull(returnedOrders);
    }

    [Fact]
    public async Task GetOrderByIdForUser_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "test@example.com";
        _orderServiceMock.Setup(service => service.GetOrderByIdAsync(id, email))
            .ReturnsAsync((Order)null);

        // Act
        var result = await _controller.GetOrderByIdForUser(id);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, apiResponse.StatusCode);
    }
}