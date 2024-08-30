using System.Security.Claims;
using System.Text;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.WebAPI.Controllers;
using FashionClothesAndTrends.WebAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Stripe;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class PaymentsControllerTests
{
    private readonly Mock<IPaymentService> _paymentServiceMock;
    private readonly Mock<ILogger<PaymentsController>> _loggerMock;
    private readonly PaymentsController _controller;
    private readonly string _whSecret = "whsec_test_secret";

    public PaymentsControllerTests()
    {
        _paymentServiceMock = new Mock<IPaymentService>();
        _loggerMock = new Mock<ILogger<PaymentsController>>();

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(config => config.GetSection("StripeSettings:WhSecret").Value).Returns(_whSecret);

        _controller = new PaymentsController(_paymentServiceMock.Object, _loggerMock.Object, configMock.Object);

        // Set up the user context
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
    public async Task CreateOrUpdatePaymentIntent_ReturnsBadRequest_WhenBasketIsNull()
    {
        // Arrange
        var basketId = "test_basket";
        _paymentServiceMock.Setup(service => service.CreateOrUpdatePaymentIntent(basketId))
            .ReturnsAsync((CustomerBasket)null);

        // Act
        var result = await _controller.CreateOrUpdatePaymentIntent(basketId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Equal("Problem with your basket", apiResponse.Message);
    }

    [Fact]
    public async Task CreateOrUpdatePaymentIntent_ReturnsBasket_WhenSuccessful()
    {
        // Arrange
        var basketId = "test_basket";
        var basket = new CustomerBasket(basketId);
        _paymentServiceMock.Setup(service => service.CreateOrUpdatePaymentIntent(basketId))
            .ReturnsAsync(basket);

        // Act
        var result = await _controller.CreateOrUpdatePaymentIntent(basketId);

        // Assert
        Assert.NotNull(result);
        var actionResult = Assert.IsType<ActionResult<CustomerBasket>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedBasket = Assert.IsType<CustomerBasket>(okResult.Value);
        Assert.Equal(basketId, returnedBasket.Id);
    }

    [Fact]
    public async Task StripeWebhook_ReturnsBadRequest_WhenStripeExceptionOccurs()
    {
        // Arrange
        var json = "{}";
        var signature = "test_signature";
        _controller.ControllerContext.HttpContext.Request.Headers["Stripe-Signature"] = signature;
        _controller.ControllerContext.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));

        _paymentServiceMock.Setup(service => service.UpdateOrderPaymentSucceeded(It.IsAny<string>()))
            .ThrowsAsync(new StripeException());

        // Act
        var result = await _controller.StripeWebhook();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, apiResponse.StatusCode);
        Assert.Equal("Stripe webhook error", apiResponse.Message);
    }
}